using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Resource.Base;
using Task.Manager.Domain.Resource.Request;
using Task.Manager.Domain.Resource.Response;

namespace TaskManager.Service.Interface.Services
{
    public interface ITaskAuditService
    {
        ResponseDefault<TaskAuditResponse[]> List(long taskId);
        ResponseDefault<TaskAuditResponse> Create(TaskAuditRequest request);
        ResponseDefault<TaskAuditResponse> CreateWithComplete(TaskAuditRequest request);

    }
}
