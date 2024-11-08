using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Entities;
using Task.Manager.Domain.Resource.Base;

namespace Task.Manager.Domain.Resource.Request
{
    public class TaskAuditRequest : RequestBase
    {
        public long TaskId { get; set; }
        public virtual TaskRequest Task { get; set; }
        public long UserId { get; set; }
        public virtual UserRequest User { get; set; }
        public string Description { get; set; }
    }
}
