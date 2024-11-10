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
    public class ProjectService : Service<Project, ProjectRequest, ProjectResponse>
    {
        public ProjectService(IHttpContextAccessor httpContextAccessor, IUnityOfWork uow, IMapper mapper, IUnityOfWork unityOfWork, IConfiguration config, IMemoryCache memoryCache) : base(httpContextAccessor, uow, mapper, unityOfWork, config, memoryCache)
        {
        }

        public ResponseDefault<ProjectResponse[]> ListByUser(ProjectGETRequest request)
        {
            var projects = Uow.Project.GetDbSet().Include(x => x.User).Where(x => x.UserId == request.UserId).ToArray();

            var response = new ResponseDefault<ProjectResponse[]>(Mapper.Map<ProjectResponse[]>(projects));

            return response;
        }

        public ResponseDefault<ProjectResponse> Create(ProjectRequest request)
        {
            var entity = Mapper.Map<Project>(request);

            Uow.Project.Create(entity);
            Uow.Complete();

            var entitySave = Mapper.Map<ProjectResponse>(entity);

            var response = new ResponseDefault<ProjectResponse>(entitySave);

            return response;
        }

        public ResponseDefault<ProjectResponse> Update(ProjectRequest request)
        {
            var entity = Uow.Project.GetFirst(x => x.Id == request.Id);

            if (entity == null)
                throw new ValidationException("Projeto não encontrado");

            Mapper.Map(request, entity);


            Uow.Project.Update(entity);
            Uow.Complete();

            var requestSave = Mapper.Map<ProjectResponse>(entity);
            var retorno = new ResponseDefault<ProjectResponse>(requestSave);

            return retorno;
        }

        public ResponseDefault<ProjectResponse> Delete(ProjectRequest request)
        {
            var entity = Uow.Project.GetDbSet().Include(x => x.Tasks)
                    .FirstOrDefault(x => x.Id == request.Id);

            if (entity == null)
                throw new ValidationException("Projeto não encontrado");

            ProjectValidation.ValidateToDelete(entity).ThrowException();

            Uow.Project.Delete(entity);
            Uow.Complete();

            var retorno = new ResponseDefault<ProjectResponse>();

            return retorno;
        }
    }
}
