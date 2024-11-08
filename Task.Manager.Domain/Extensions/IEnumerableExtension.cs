using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Task.Manager.Domain.Extensions
{
    public static class IEnumerableExtension
    {
        public static void ThrowException(this IEnumerable<string> errors)
        {
            if (errors.Any())
            {
                string erro = string.Join(",", errors);

                throw new ValidationException(erro);
            }
        }
    }
}
