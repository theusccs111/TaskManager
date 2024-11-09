using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Entities;
using Task.Manager.Domain.Extensions;
using Task.Manager.Domain.Resource.Base;
using Task.Manager.Domain.Resource.Request;
using Task.Manager.Domain.Resource.Response;
using Task.Manager.Domain.Validations;
using TaskManager.Service.Interface.Persistance;

namespace TaskManager.Service.Services
{
    public class TaskAuditService : Service<TaskAudit, TaskAuditRequest, TaskAuditResponse>
    {
        public TaskAuditService(IHttpContextAccessor httpContextAccessor, IUnityOfWork uow, IMapper mapper, IUnityOfWork unityOfWork, IConfiguration config, IMemoryCache memoryCache) : base(httpContextAccessor, uow, mapper, unityOfWork, config, memoryCache)
        {
        }

        public void List()
        {

        }

        public ResponseDefault<TaskAuditResponse> Create(TaskAuditRequest request)
        {
            var entity = Mapper.Map<TaskAudit>(request);

            Uow.TaskAudit.Create(entity);

            var entitySave = Mapper.Map<TaskAuditResponse>(entity);

            var response = new ResponseDefault<TaskAuditResponse>(entitySave);

            return response;
        }

        public ResponseDefault<TaskAuditResponse> CreateWithComplete(TaskAuditRequest request)
        {
            var entity = Mapper.Map<TaskAudit>(request);

            Uow.TaskAudit.Create(entity);
            Uow.Complete();

            var entitySave = Mapper.Map<TaskAuditResponse>(entity);

            var response = new ResponseDefault<TaskAuditResponse>(entitySave);

            return response;
        }

        public void Delete()
        {

        }
    }
}
