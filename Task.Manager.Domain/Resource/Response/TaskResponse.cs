﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Entities.Base;
using Task.Manager.Domain.Enums;
using Task.Manager.Domain.Resource.Base;

namespace Task.Manager.Domain.Resource.Response
{
    public class TaskResponse : ResponseBase
    {
        public long ProjectId { get; set; }
        public virtual ProjectResponse Project { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpiredDate { get; set; }
        public StatusTask Status { get; set; }
    }
}