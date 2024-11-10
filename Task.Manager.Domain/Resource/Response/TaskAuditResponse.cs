using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Entities;
using Task.Manager.Domain.Resource.Base;
using Task.Manager.Domain.Resource.Request;

namespace Task.Manager.Domain.Resource.Response
{
    public class TaskAuditResponse : ResponseBase
    {
        public long TaskId { get; set; }
        public virtual TaskResponse Task { get; set; }
        public long UserId { get; set; }
        public virtual UserResponse User { get; set; }
        public string Description { get; set; }
    }
}
