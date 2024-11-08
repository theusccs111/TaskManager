using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;
using SOMA.OPEX.Domain.Resource.Base;
using SOMA.OPEX.Service.Interface.Persistance;
using SOMA.OPEX.Domain.Entities.Base;
using Microsoft.Extensions.Configuration;
using SOMA.OPEX.Domain.Resource.Response;
using SOMA.OPEX.Service.Helpers;
using Serilog;
using SOMA.OPEX.Service.Interface.Persistance.Repository;
using Task.Manager.Domain.Entities.Base;
using Task.Manager.Domain.Resource.Base;
using TaskManager.Service.Interface.Persistance;
using System.Collections.Generic;
using System;

namespace TaskManager.Service.Services
{
    public abstract class Service<T, RQ, RP> where T : Entity where RQ : RequestBase where RP : ResponseBase
    {
        private readonly IUnityOfWork _uow;
        protected IUnityOfWork Uow { get { return _uow; } }

        private readonly IMapper _mapper;
        protected IMapper Mapper { get { return _mapper; } }

        private readonly IConfiguration _config;
        protected IConfiguration Config { get { return _config; } }
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected IHttpContextAccessor HttpContextAccessor { get { return _httpContextAccessor; } }
        public Service(IHttpContextAccessor httpContextAccessor, IUnityOfWork uow, IMapper mapper,
            IUnityOfWork unityOfWork, IConfiguration config,
            IMemoryCache memoryCache)
        {
            _uow = uow;
            _mapper = mapper;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual IEnumerable<RP> Get()
        {
            try
            {
                var dados = _uow.Repository<T>().GetAll();
                return Mapper.Map<IEnumerable<RP>>(dados);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual T GetById(int Id)
        {
            try
            {
                return _uow.Repository<T>().GetFirst(e => e.Id == Id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual RQ Add(RQ request)
        {
            try
            {
                if (request.Id > 0)
                {
                    throw new ValidationException(string.Concat("Ao adicionar ", typeof(T).Name, " o id deve ser 0 ou nulo."));
                }
                var entity = Mapper.Map<T>(request);

                Uow.Repository<T>().Create(entity);

                return request;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public virtual void AddMany(RQ[] request)
        {
            foreach (var item in request)
            {
                Add(item);
            }
        }

        public virtual void Update(RQ request)
        {
            try
            {
                var entity = Mapper.Map<T>(request);

                Uow.Repository<T>().Update(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual void UpdateMany(RQ[] request)
        {
            foreach (var item in request)
            {
                Update(item);
            }
        }

        public virtual void Delete(long id)
        {
            try
            {
                var entity = Uow.Repository<T>().GetFirst(x => x.Id == id);
                Uow.Repository<T>().Delete(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual void DeleteMany(long[] ids)
        {
            foreach (var id in ids)
            {
                Delete(id);
            }
        }

        public virtual void Complete()
        {
            Uow.Complete();
        }

    }
}