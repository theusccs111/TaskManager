using AutoMapper;
using System.Linq;
using Task.Manager.Domain.Entities;
using Task.Manager.Domain.Resource.Request;
using Task.Manager.Domain.Resource.Response;

namespace Task.Manager.Domain
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            var entityAssemplyEntity = typeof(User).Assembly;
            var entityAssemplyRequest = typeof(UserRequest).Assembly.ExportedTypes.ToList();
            var entityAssemplyResponse = typeof(UserResponse).Assembly.ExportedTypes.ToList();

            entityAssemplyEntity.ExportedTypes.ToList().ForEach(s =>
            {
                var formattedRequestModelName = string.Format("{0}Request", s.Name);
                var requestModelName = entityAssemplyRequest.FirstOrDefault(s => s.Name == formattedRequestModelName);
                if (requestModelName != null)
                {
                    this.CreateMap(s, requestModelName).ReverseMap();
                }

                var formattedResponseName = string.Format("{0}Response", s.Name);
                var responseModelName = entityAssemplyResponse.FirstOrDefault(s => s.Name == formattedResponseName);
                if (responseModelName != null)
                {
                    this.CreateMap(s, responseModelName).ReverseMap();
                }
            });
        }
    }
}
