using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
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

    }
}