using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text;
using Task.Manager.Domain.Entities;
using Task.Manager.Domain.Extensions;
using Task.Manager.Domain.Resource.Base;
using Task.Manager.Domain.Resource.Request;
using Task.Manager.Domain.Resource.Response;
using Task.Manager.Domain.Validations;
using TaskManager.Service.Interface.Persistance;

namespace TaskManager.Service.Services
{
    public class TaskService : Service<Task.Manager.Domain.Entities.Task, TaskRequest, TaskResponse>
    {
        private readonly TaskAuditService _taskAuditService;
        public TaskService(IHttpContextAccessor httpContextAccessor, IUnityOfWork uow, IMapper mapper, IUnityOfWork unityOfWork, IConfiguration config, IMemoryCache memoryCache, TaskAuditService taskAuditService) : base(httpContextAccessor, uow, mapper, unityOfWork, config, memoryCache)
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

        public void ReportPerformance()
        {
            //- A API deve fornecer endpoints para gerar relatórios de desempenho, como o número médio de tarefas concluídas por usuário nos últimos 30 dias.
            // -Os relatórios devem ser acessíveis apenas por usuários com uma função específica de "gerente".


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

            TaskValidation.ValidateToEdit(entity, request).ThrowException();
            string description = GetAuditDescription(entity, request);

            Mapper.Map(request, entity);


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

            Uow.Task.Delete(entity);
            Uow.Complete();

            var retorno = new ResponseDefault<TaskResponse>();

            return retorno;
        }

    }
}
