using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Entities;
using Task.Manager.Domain.Entities.Base;
using Task.Manager.Domain.Resource.Base;

namespace Task.Manager.Domain.Resource.Response
{
    public class ProjectResponse : ResponseBase
    {
        //public virtual ICollection<TaskResponse> Tasks { get; set; }
        public long UserId { get; set; }
        public string Description { get; set; }
    }
}
