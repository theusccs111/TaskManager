using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Entities;
using Task.Manager.Domain.Entities.Base;
using Task.Manager.Domain.Enums;
using Task.Manager.Domain.Resource.Base;

namespace Task.Manager.Domain.Resource.Request
{
    public class TaskRequest : RequestBase
    {
        public long ProjectId { get; set; }
        public long UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpiredDate { get; set; }
        public StatusTask Status { get; set; }
        public PriorityTask Priority { get; set; }
    }
}
