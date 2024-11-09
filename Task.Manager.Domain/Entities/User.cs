using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Entities.Base;
using Task.Manager.Domain.Enums;

namespace Task.Manager.Domain.Entities
{
    public class User : Entity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public RoleUser Role { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<TaskAudit> TaskAudits { get; set; }

    }
}
