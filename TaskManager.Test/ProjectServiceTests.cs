using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Entities;
using TaskManager.Service.Interface.Persistance;
using TaskManager.Service.Services;
using Xunit;
using Microsoft.Extensions.Configuration;
using Task.Manager.Domain.Resource.Request;
using Task.Manager.Domain.Resource.Response;
using System.Linq.Expressions;
using Moq.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Task.Manager.Domain.Enums;

namespace TaskManager.Test
{
    public class ProjectServiceTests
    {
        private readonly ProjectService _projectService;
        private readonly Mock<IUnityOfWork> _mockUow;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly Mock<IMemoryCache> _mockMemoryCache;

        public ProjectServiceTests()
        {
            _mockUow = new Mock<IUnityOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockConfig = new Mock<IConfiguration>();
            _mockMemoryCache = new Mock<IMemoryCache>();

            _projectService = new ProjectService(
                _mockHttpContextAccessor.Object,
                _mockUow.Object,
                _mockMapper.Object,
                _mockUow.Object,
                _mockConfig.Object,
                _mockMemoryCache.Object
            );
        }

        [Fact]
        public void Create_ShouldReturnError_WhenCreateFails()
        {
            // Arrange
            var projectRequest = new ProjectRequest { Description = "New Project" };
            _mockMapper.Setup(m => m.Map<Project>(It.IsAny<ProjectRequest>())).Throws(new Exception("Mapping failed"));

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _projectService.Create(projectRequest));
            Assert.Equal("Mapping failed", exception.Message);
        }

        [Fact]
        public void Update_ShouldReturnError_WhenProjectNotFound()
        {
            // Arrange
            var projectRequest = new ProjectRequest { Id = 1, Description = "Updated Project" };

            _mockUow.Setup(uow => uow.Project.GetFirst(It.IsAny<Expression<Func<Project, bool>>>())).Returns((Project)null);

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => _projectService.Update(projectRequest));

            Assert.Equal("Projeto não encontrado", exception.Message);
        }


        [Fact]
        public void Delete_ShouldReturnError_WhenProjectNotFound()
        {
            // Arrange
            var projectRequest = new ProjectRequest { Id = 999 }; 
            _mockUow.Setup(uow => uow.Project.GetDbSet()).ReturnsDbSet(new List<Project>());

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => _projectService.Delete(projectRequest));

            Assert.Equal("Projeto não encontrado", exception.Message);
        }
        
        [Fact]
        public void ListByUser_ShouldReturnEmpty_WhenUserHasNoProjects()
        {
            // Arrange
            var userId = 1;
            _mockUow.Setup(uow => uow.Project.GetDbSet()).ReturnsDbSet(new List<Project>());

            // Act
            var result = _projectService.ListByUser(new ProjectGETRequest { UserId = userId });

            // Assert
            Assert.NotNull(result.Data);
            Assert.Empty(result.Data); 
        }

        [Fact]
        public void Delete_ShouldThrowValidationException_WhenProjectHasPendingTasks()
        {
            // Arrange
            var projectId = 1;
            var project = new Project
            {
                Id = projectId,
                Description = "Projeto Teste",
                Tasks = new List<Task.Manager.Domain.Entities.Task>
                {
                    new Task.Manager.Domain.Entities.Task { Id = 1, Status = StatusTask.Proposed },
                    new Task.Manager.Domain.Entities.Task { Id = 2, Status = StatusTask.Active }
                }
            };

            _mockUow.Setup(uow => uow.Project.GetDbSet()).ReturnsDbSet(new List<Project> { project });

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => _projectService.Delete(new ProjectRequest { Id = projectId }));
            Assert.Equal("O projeto Projeto Teste tem tarefas pendentes nele, conclua as tarefas ou remova as tarefas do projeto.", exception.Message);
        }

        [Fact]
        public void Delete_ShouldDeleteProject_WhenNoPendingTasks()
        {
            // Arrange
            var projectId = 2;
            var project = new Project
            {
                Id = projectId,
                Description = "Projeto Teste Sem Tarefas Pendentes",
                Tasks = new List<Task.Manager.Domain.Entities.Task>
                {
                    new Task.Manager.Domain.Entities.Task { Id = 1, Status = StatusTask.Closed },
                    new Task.Manager.Domain.Entities.Task { Id = 2, Status = StatusTask.Closed }
                }
            };

            _mockUow.Setup(uow => uow.Project.GetDbSet()).ReturnsDbSet(new List<Project> { project });
            _mockUow.Setup(uow => uow.Project.Delete(It.IsAny<Project>()));

            // Act
            var result = _projectService.Delete(new ProjectRequest { Id = projectId });

            // Assert
            Assert.NotNull(result);
            _mockUow.Verify(uow => uow.Project.Delete(It.IsAny<Project>()), Times.Once);
            _mockUow.Verify(uow => uow.Complete(), Times.Once);
        }

        [Fact]
        public void Delete_ShouldThrowValidationException_WhenProjectNotFound()
        {
            // Arrange
            var projectId = 999; 
            _mockUow.Setup(uow => uow.Project.GetDbSet()).ReturnsDbSet(new List<Project>());

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => _projectService.Delete(new ProjectRequest { Id = projectId }));
            Assert.Equal("Projeto não encontrado", exception.Message);
        }

    }
}
