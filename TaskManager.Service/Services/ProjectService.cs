using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Entities;
using Task.Manager.Domain.Resource.Request;
using Task.Manager.Domain.Resource.Response;
using TaskManager.Service.Interface.Persistance;

namespace TaskManager.Service.Services
{
    public class ProjectService : Service<Project, ProjectRequest, ProjectResponse>
    {
        public ProjectService(IHttpContextAccessor httpContextAccessor, IUnityOfWork uow, IMapper mapper, IUnityOfWork unityOfWork, IConfiguration config, IMemoryCache memoryCache) : base(httpContextAccessor, uow, mapper, unityOfWork, config, memoryCache)
        {
        }

        public void ListByUser()
        {

        }

        public int Create(ProjectRequest request)
        {
            return 1;
        }

        public void Update()
        {

        }

        public void Delete()
        {
            //Um projeto não pode ser removido se ainda houver tarefas pendentes associadas a ele.
            //Caso o usuário tente remover um projeto com tarefas pendentes, a API deve retornar um erro e sugerir a conclusão ou remoção das tarefas primeiro.

        }
    }
}
