using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Entities;
using Task.Manager.Domain.Entities.Base;
using Task.Manager.Domain.Resource.Base;

namespace Task.Manager.Domain.Resource.Request
{
    public class ProjectRequest : RequestBase
    {
        public long UserId { get; set; }
        public string Description { get; set; }
    }
}
