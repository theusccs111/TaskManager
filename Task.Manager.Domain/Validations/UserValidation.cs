using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Entities;

namespace Task.Manager.Domain.Validations
{
    public static class UserValidation
    {
        public static IEnumerable<string> ValidateBeManager(User user)
        {
            if (user.Role != Enums.RoleUser.Manager)
                yield return $"O usuário não é gerente, portanto acesso negado";
        }
    }
}
