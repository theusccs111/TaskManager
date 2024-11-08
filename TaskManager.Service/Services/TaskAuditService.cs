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
using Task.Manager.Domain.Resource.Request;
using Task.Manager.Domain.Resource.Response;
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

        public void Create()
        {
            //-Os usuários podem adicionar comentários a uma tarefa para fornecer informações adicionais.
            //- Os comentários devem ser registrados no histórico de alterações da tarefa.
        }

        public void Update()
        {
            //Não é permitido alterar a prioridade de uma tarefa depois que ela foi criada.
        }

        public void Delete()
        {

        }
    }
}
