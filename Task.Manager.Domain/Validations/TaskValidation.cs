using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Entities;
using Task.Manager.Domain.Resource.Request;

namespace Task.Manager.Domain.Validations
{
    public static class TaskValidation
    {
        public static IEnumerable<string> ValidateToSave(Project project, int countTasks)
        {
            if(countTasks >= 20)
                yield return $"O projeto {project.Description} tem 20 tarefas já cadastradas, então não é possível cadastrar outra tarefa.";
        }
        public static IEnumerable<string> ValidateToEdit(Entities.Task taskOriginal, TaskRequest taskToSave)
        {
            if (taskOriginal.Priority != taskToSave.Priority)
                yield return $"Não é possível alterar a prioridade da tarefa depois de criada.";
        }
    }
}
