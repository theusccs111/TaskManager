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
using Task.Manager.Domain.Enums;
using TaskManager.Service.Interface.Services;
using Task.Manager.Domain.Validations;

namespace TaskManager.Test
{
    public class TaskServiceTests
    {
        private readonly Mock<IUnityOfWork> _mockUow;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ITaskAuditService> _mockTaskAuditService;
        private readonly TaskService _taskService;

        public TaskServiceTests()
        {
            _mockUow = new Mock<IUnityOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockTaskAuditService = new Mock<ITaskAuditService>();
            _taskService = new TaskService(
                new Mock<IHttpContextAccessor>().Object,
                _mockUow.Object,
                _mockMapper.Object,
                _mockUow.Object,
                new Mock<IConfiguration>().Object,
                new Mock<IMemoryCache>().Object,
                _mockTaskAuditService.Object);
        }

        [Fact]
        public void Create_ShouldThrowException_WhenProjectHas20Tasks()
        {
            // Arrange
            var project = new Project { Id = 1, Description = "Project 1" };
            var countTasks = 20;
            var taskRequest = new TaskRequest
            {
                ProjectId = project.Id,
                Title = "New Task",
                UserId = 1,
                Status = StatusTask.Proposed,
                ExpiredDate = DateTime.Now.AddDays(10)
            };

            _mockUow.Setup(uow => uow.Project.GetFirst(It.IsAny<Expression<Func<Project, bool>>>())).Returns(project);
            _mockUow.Setup(uow => uow.Task.Get(It.IsAny<Expression<Func<Task.Manager.Domain.Entities.Task, bool>>>()))
     .Returns(new List<Task.Manager.Domain.Entities.Task>(new Task.Manager.Domain.Entities.Task[countTasks]).AsQueryable());

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => _taskService.Create(taskRequest));
            Assert.Contains("O projeto", exception.Message);
        }

        [Fact]
        public void Create_ShouldCreateTask_WhenValid()
        {
            // Arrange
            var project = new Project { Id = 1, Description = "Project 1" };
            var taskRequest = new TaskRequest
            {
                ProjectId = project.Id,
                Title = "New Task",
                UserId = 1,
                Status = StatusTask.Proposed,
                ExpiredDate = DateTime.Now.AddDays(10)
            };

            var taskEntity = new Task.Manager.Domain.Entities.Task
            {
                Id = 1,
                Title = taskRequest.Title,
                ProjectId = taskRequest.ProjectId,
                Status = taskRequest.Status,
                ExpiredDate = taskRequest.ExpiredDate,
                UserId = taskRequest.UserId
            };

            _mockUow.Setup(uow => uow.Project.GetFirst(It.IsAny<Expression<Func<Project, bool>>>())).Returns(project);
            _mockUow.Setup(uow => uow.Task.Get(It.IsAny<Expression<Func<Task.Manager.Domain.Entities.Task, bool>>>())).Returns(new List<Task.Manager.Domain.Entities.Task>().AsQueryable());
            _mockMapper.Setup(mapper => mapper.Map<Task.Manager.Domain.Entities.Task>(taskRequest)).Returns(taskEntity);

            // Act
            var result = _taskService.Create(taskRequest);

            // Assert
            Assert.NotNull(result);
            _mockUow.Verify(uow => uow.Task.Create(It.IsAny<Task.Manager.Domain.Entities.Task>()), Times.Once);
            _mockUow.Verify(uow => uow.Complete(), Times.Once);
        }

        [Fact]
        public void Update_ShouldThrowException_WhenPriorityChanges()
        {
            // Arrange
            var taskRequest = new TaskRequest
            {
                Id = 1,
                ProjectId = 1,
                Title = "Updated Task",
                UserId = 1,
                ExpiredDate = DateTime.Now.AddDays(10),
                Status = StatusTask.Proposed,
                Priority = PriorityTask.High
            };

            var taskOriginal = new Task.Manager.Domain.Entities.Task
            {
                Id = 1,
                Title = "Old Task",
                ProjectId = 1,
                UserId = 1,
                ExpiredDate = DateTime.Now.AddDays(5),
                Status = StatusTask.Proposed,
                Priority = PriorityTask.Low
            };

            _mockUow.Setup(uow => uow.Task.GetFirst(It.IsAny<Expression<Func<Task.Manager.Domain.Entities.Task, bool>>>())).Returns(taskOriginal);

            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => _taskService.Update(taskRequest));
            Assert.Equal("Não é possível alterar a prioridade da tarefa depois de criada.", exception.Message);
        }

        [Fact]
        public void Update_ShouldUpdateTask_WhenValid()
        {
            // Arrange
            var taskRequest = new TaskRequest
            {
                Id = 1,
                ProjectId = 1,
                Title = "Updated Task",
                UserId = 1,
                ExpiredDate = DateTime.Now.AddDays(10),
                Status = StatusTask.Proposed,
                Priority = PriorityTask.Low
            };

            var taskOriginal = new Task.Manager.Domain.Entities.Task
            {
                Id = 1,
                Title = "Old Task",
                ProjectId = 1,
                UserId = 1,
                ExpiredDate = DateTime.Now.AddDays(5),
                Status = StatusTask.Proposed,
                Priority = PriorityTask.Low
            };

            var taskUpdated = new Task.Manager.Domain.Entities.Task
            {
                Id = 1,
                Title = taskRequest.Title,
                ProjectId = taskRequest.ProjectId,
                UserId = taskRequest.UserId,
                ExpiredDate = taskRequest.ExpiredDate,
                Status = taskRequest.Status,
                Priority = taskRequest.Priority
            };

            _mockUow.Setup(uow => uow.Task.GetFirst(It.IsAny<Expression<Func<Task.Manager.Domain.Entities.Task, bool>>>())).Returns(taskOriginal);
            _mockMapper.Setup(mapper => mapper.Map(taskRequest, taskOriginal));
            _mockMapper.Setup(mapper => mapper.Map<TaskResponse>(taskUpdated)).Returns(new TaskResponse { Id = 1, Title = taskRequest.Title });

            // Act
            var result = _taskService.Update(taskRequest);

            // Assert
            Assert.NotNull(result);
            _mockUow.Verify(uow => uow.Task.Update(It.IsAny<Task.Manager.Domain.Entities.Task>()), Times.Once);
            _mockUow.Verify(uow => uow.Complete(), Times.Once);
            _mockTaskAuditService.Verify(service => service.Create(It.IsAny<TaskAuditRequest>()), Times.Once);
        }

        [Fact]
        public void Delete_ShouldDeleteTask_WhenValid()
        {
            // Arrange
            var taskRequest = new TaskRequest { Id = 1 };
            var taskEntity = new Task.Manager.Domain.Entities.Task { Id = 1 };

            _mockUow.Setup(uow => uow.Task.GetFirst(It.IsAny<Expression<Func<Task.Manager.Domain.Entities.Task, bool>>>())).Returns(taskEntity);

            // Act
            var result = _taskService.Delete(taskRequest);

            // Assert
            Assert.NotNull(result);
            _mockUow.Verify(uow => uow.Task.Delete(It.IsAny<Task.Manager.Domain.Entities.Task>()), Times.Once);
            _mockUow.Verify(uow => uow.Complete(), Times.Once);
        }
    }

}
