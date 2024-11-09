using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Task.Manager.Domain.Entities;
using Task.Manager.Domain.Enums;
using Task.Manager.Domain.Extensions;
using Task.Manager.Domain.Resource.Base;
using Task.Manager.Domain.Resource.Request;
using Task.Manager.Domain.Resource.Response;
using Task.Manager.Domain.Validations;
using TaskManager.Service.Interface.Persistance;
using TaskManager.Service.Interface.Services;

namespace TaskManager.Service.Services
{
    public class TaskService : Service<Task.Manager.Domain.Entities.Task, TaskRequest, TaskResponse>
    {
        private readonly ITaskAuditService _taskAuditService;
        public TaskService(IHttpContextAccessor httpContextAccessor, IUnityOfWork uow, IMapper mapper, IUnityOfWork unityOfWork, IConfiguration config, IMemoryCache memoryCache, ITaskAuditService taskAuditService) : base(httpContextAccessor, uow, mapper, unityOfWork, config, memoryCache)
        {
            _taskAuditService = taskAuditService;
        }

        public ResponseDefault<TaskResponse[]> ListByProject(TaskGETRequest request)
        {
            var tasks = Uow.Task.Get(x => x.ProjectId == request.ProjectId).ToArray();

            ResponseDefault<TaskResponse[]> response = new ResponseDefault<TaskResponse[]>()
            {
                Data = Mapper.Map<TaskResponse[]>(tasks),
                Message = "Lista de tarefas por projeto",
                Success = true
            };

            return response;
        }

        public ResponseDefault<List<ReportPerformanceResponse>> ReportPerformance(ReportRequest request)
        {
            var user = Uow.User.GetFirst(x => x.Id == request.UserId);

            UserValidation.ValidateBeManager(user).ThrowException();

            var tasks = Uow.Task.GetDbSet()
                .Where(x => x.Status == StatusTask.Closed && x.ClosedDate >= DateTime.Now.AddDays(-30))
                .GroupBy(x => x.UserId)
                .Select(g => new ReportPerformanceResponse()
                {
                    UserId = g.Key,
                    AverageTasksCompleted = g.Count()
                })
            .ToList();

            var response = new ResponseDefault<List<ReportPerformanceResponse>>(tasks);

            return response;

        }

        public ResponseDefault<TaskResponse> Create(TaskRequest request)
        {
            var entity = Mapper.Map<Task.Manager.Domain.Entities.Task>(request);
            int countTasks = Uow.Task.Get(x => x.ProjectId == request.ProjectId).Count();
            Project project = Uow.Project.GetFirst(x => x.Id == request.ProjectId);

            TaskValidation.ValidateToSave(project, countTasks).ThrowException();

            Uow.Task.Create(entity);
            Uow.Complete();

            var entitySave = Mapper.Map<TaskResponse>(entity);

            var response = new ResponseDefault<TaskResponse>(entitySave);

            return response;
        }

        public ResponseDefault<TaskResponse> Update(TaskRequest request)
        {
            var entity = Uow.Task.GetFirst(x => x.Id == request.Id);

            if (entity == null)
                throw new ValidationException("Tarefa não encontrada");

            TaskValidation.ValidateToEdit(entity, request).ThrowException();
            string description = GetAuditDescription(entity, request);

            Mapper.Map(request, entity);
            if(entity.Status == StatusTask.Closed)
            {
                entity.ClosedDate = DateTime.Now;
            }

            Uow.Task.Update(entity);
            

            _taskAuditService.Create(new TaskAuditRequest()
            {
                TaskId = entity.Id,
                UserId = request.UserId,
                Description = description
            });

            Uow.Complete();

            var requestSave = Mapper.Map<TaskResponse>(entity);
            var retorno = new ResponseDefault<TaskResponse>(requestSave);

            return retorno;
        }

        private string GetAuditDescription(Task.Manager.Domain.Entities.Task entity, TaskRequest request)
        {
            var texto = new StringBuilder();

            texto.AppendFormat(
                "A tarefa \"{0}\" sofreu as seguintes alterações: "
                , entity.Title);

            if (entity.ProjectId != request.ProjectId)
                texto.AppendFormat(
                    "O Id do projeto foi alterado para \"{0}\". "
                    , request.ProjectId);

            if (entity.Title != request.Title)
                texto.AppendFormat(
                    "O Título foi alterado para \"{0}\". "
                    , request.Title);

            if (entity.Description != request.Description)
                texto.AppendFormat(
                    "A Descrição foi alterado para \"{0}\". "
                    , request.Description);

            if (entity.ExpiredDate != request.ExpiredDate)
                texto.AppendFormat(
                    "A data de prazo foi alterado para \"{0}\". "
                    , request.ExpiredDate);

            if (entity.Status != request.Status)
                texto.AppendFormat(
                    "O Status foi alterado para \"{0}\". "
                    , request.Status.GetDescription());

            return texto.ToString();
        }

        public ResponseDefault<TaskResponse> Delete(TaskRequest request)
        {
            var entity = Uow.Task.GetFirst(x => x.Id == request.Id);

            if (entity == null)
                throw new ValidationException("Tarefa não encontrada");

            Uow.Task.Delete(entity);
            Uow.Complete();

            var retorno = new ResponseDefault<TaskResponse>();

            return retorno;
        }

    }
}
