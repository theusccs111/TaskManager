using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Entities;
using Task.Manager.Domain.Resource.Base;
using Task.Manager.Domain.Resource.Request;
using Task.Manager.Domain.Resource.Response;
using TaskManager.Service.Interface.Persistance;

namespace TaskManager.Service.Services
{
    public class TaskService : Service<Task.Manager.Domain.Entities.Task, TaskRequest, TaskResponse>
    {
        public TaskService(IHttpContextAccessor httpContextAccessor, IUnityOfWork uow, IMapper mapper, IUnityOfWork unityOfWork, IConfiguration config, IMemoryCache memoryCache) : base(httpContextAccessor, uow, mapper, unityOfWork, config, memoryCache)
        {
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

        public void Create()
        {
            //Cada projeto tem um limite máximo de 20 tarefas. Tentar adicionar mais tarefas do que o limite deve resultar em um erro.
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
