using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Entities.Base;

namespace Task.Manager.Domain.Entities
{
    public class Project : Entity
    {
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
