using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Entities.Base;
using Task.Manager.Domain.Resource.Base;

namespace Task.Manager.Domain.Resource.Response
{
    public class UserResponse : ResponseBase
    {
        public string Login { get; set; }
        public string Password { get; set; }

    }
}
