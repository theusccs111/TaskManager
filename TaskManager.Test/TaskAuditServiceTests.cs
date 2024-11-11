using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Task.Manager.Domain.Entities;
using Task.Manager.Domain.Resource.Request;
using Task.Manager.Domain.Resource.Response;
using TaskManager.Service.Interface.Persistance;
using TaskManager.Service.Services;
using Xunit;

namespace TaskManager.Test
{
    public class TaskAuditServiceTests
        {
            private readonly TaskAuditService _taskAuditService;
            private readonly Mock<IUnityOfWork> _mockUow;
            private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
            private readonly Mock<IMapper> _mockMapper;
            private readonly Mock<IMemoryCache> _mockMemoryCache;
            private readonly Mock<IConfiguration> _mockConfig;

            public TaskAuditServiceTests()
            {
                _mockUow = new Mock<IUnityOfWork>();
                _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
                _mockMapper = new Mock<IMapper>();
                _mockMemoryCache = new Mock<IMemoryCache>();
                _mockConfig = new Mock<IConfiguration>();

                _taskAuditService = new TaskAuditService(
                    _mockHttpContextAccessor.Object,
                    _mockUow.Object,
                    _mockMapper.Object,
                    _mockUow.Object,
                    _mockConfig.Object,
                    _mockMemoryCache.Object
                );
            }

            [Fact]
            public void List_ShouldReturnTaskAuditList_WhenTaskExists()
            {
                // Arrange
                var taskId = 1L;
                var taskAuditEntities = new List<TaskAudit>
            {
                new TaskAudit { TaskId = taskId, Id = 1, Description = "Audit 1" },
                new TaskAudit { TaskId = taskId, Id = 2, Description = "Audit 2" }
            };
                _mockUow.Setup(uow => uow.TaskAudit.Get(It.IsAny<Expression<Func<TaskAudit, bool>>>())).Returns(taskAuditEntities.AsQueryable());

                var taskAuditResponses = new List<TaskAuditResponse>
            {
                new TaskAuditResponse { TaskId = taskId, Id = 1, Description = "Audit 1" },
                new TaskAuditResponse { TaskId = taskId, Id = 2, Description = "Audit 2" }
            };

                _mockMapper.Setup(m => m.Map<TaskAuditResponse[]>(It.IsAny<IEnumerable<TaskAudit>>())).Returns(taskAuditResponses.ToArray());

                // Act
                var result = _taskAuditService.List(taskId);

                // Assert
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.Equal(2, result.Data.Length);
                Assert.Equal("Audit 1", result.Data[0].Description);
            }

            [Fact]
            public void Create_ShouldReturnTaskAuditResponse_WhenSuccessfullyCreated()
            {
                // Arrange
                var taskAuditRequest = new TaskAuditRequest { TaskId = 1L, Description = "New Audit" };
                var taskAuditEntity = new TaskAudit { TaskId = 1L, Description = "New Audit" };
                var taskAuditResponse = new TaskAuditResponse { TaskId = 1L, Description = "New Audit" };

                _mockMapper.Setup(m => m.Map<TaskAudit>(It.IsAny<TaskAuditRequest>())).Returns(taskAuditEntity);
                _mockMapper.Setup(m => m.Map<TaskAuditResponse>(It.IsAny<TaskAudit>())).Returns(taskAuditResponse);

                _mockUow.Setup(uow => uow.TaskAudit.Create(It.IsAny<TaskAudit>()));

                // Act
                var result = _taskAuditService.Create(taskAuditRequest);

                // Assert
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.Equal("New Audit", result.Data.Description);
            }

            [Fact]
            public void CreateWithComplete_ShouldReturnTaskAuditResponse_WhenSuccessfullyCreatedAndCompleted()
            {
                // Arrange
                var taskAuditRequest = new TaskAuditRequest { TaskId = 1L, Description = "New Audit with Complete" };
                var taskAuditEntity = new TaskAudit { TaskId = 1L, Description = "New Audit with Complete" };
                var taskAuditResponse = new TaskAuditResponse { TaskId = 1L, Description = "New Audit with Complete" };

                _mockMapper.Setup(m => m.Map<TaskAudit>(It.IsAny<TaskAuditRequest>())).Returns(taskAuditEntity);
                _mockMapper.Setup(m => m.Map<TaskAuditResponse>(It.IsAny<TaskAudit>())).Returns(taskAuditResponse);

                _mockUow.Setup(uow => uow.TaskAudit.Create(It.IsAny<TaskAudit>()));
                _mockUow.Setup(uow => uow.Complete());

                // Act
                var result = _taskAuditService.CreateWithComplete(taskAuditRequest);

                // Assert
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.Equal("New Audit with Complete", result.Data.Description);
            }
        }
}
