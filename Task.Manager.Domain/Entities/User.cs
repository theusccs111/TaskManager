using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Entities.Base;

namespace Task.Manager.Domain.Entities
{
    public class User : Entity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public RoleUser Role { get; set; }

    }
}
