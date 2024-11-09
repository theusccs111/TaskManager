using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Entities.Base;
using Task.Manager.Domain.Enums;

namespace Task.Manager.Domain.Entities
{
    public class Task : Entity
    {
        public long ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpiredDate { get; set; }
        public StatusTask Status { get; set; }
        public PriorityTask Priority { get; set; }
        public virtual ICollection<TaskAudit> TaskAudits { get; set; }
    }
}
