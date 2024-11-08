using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Entities.Base;

namespace Task.Manager.Domain.Entities
{
    public class TaskAudit : Entity
    {
        public long TaskId { get; set; }
        public virtual Task Task { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public string Description { get; set; }

    }
}
