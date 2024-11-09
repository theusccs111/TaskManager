using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Entities;

namespace Task.Manager.Domain.Validations
{
    public static class ProjectValidation
    {
        public static IEnumerable<string> ValidateToDelete(Project project)
        {
            if (project.Tasks.Any(x => x.Status == Enums.StatusTask.Proposed || x.Status == Enums.StatusTask.Active))
                yield return $"O projeto {project.Description} tem tarefas pendentes nele, conclua as tarefas ou remova as tarefas do projeto.";
        }
    }
}
