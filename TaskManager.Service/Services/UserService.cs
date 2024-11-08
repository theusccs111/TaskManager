using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using Task.Manager.Domain.Entities;
using Task.Manager.Domain.Extensions;
using Task.Manager.Domain.Resource.Base;
using Task.Manager.Domain.Resource.Request;
using Task.Manager.Domain.Resource.Response;
using TaskManager.Service.Interface.Persistance;

namespace TaskManager.Service.Services
{
    public class UserService : Service<User, UserRequest, UserResponse>
    {
        public UserService(IHttpContextAccessor httpContextAccessor, IUnityOfWork uow, IMapper mapper, IUnityOfWork unityOfWork, IConfiguration config, IMemoryCache memoryCache) : base(httpContextAccessor, uow, mapper, unityOfWork, config, memoryCache)
        {
        }

        
    }
}
