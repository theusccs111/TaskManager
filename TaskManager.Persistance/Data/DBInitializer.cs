using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Entities;
using Task.Manager.Domain.Enums;

namespace TaskManager.Persistance.Data
{
    public static class DBInitializer
    {
        public static void Initialize(TaskManagerContext context)
        {
            context.Database.EnsureCreated();

            SeedUsers(context);
            SeedProjects(context);
            SeedTasks(context);

        }

        public static void SeedUsers(TaskManagerContext context)
        {
            if (!context.User.Any())
            {
                IEnumerable<User> users = new List<User>()
            {
                new User()
                {
                    Login = "theus",
                    Password= "123456",
                    Role= RoleUser.Manager,
                },

                new User()
                {
                    Login = "jhon",
                    Password= "123456",
                    Role= RoleUser.Common,
                },
            };

                context.User.AddRange(users);
                context.SaveChanges();
            }
        }

        public static void SeedProjects(TaskManagerContext context)
        {
            if (!context.Project.Any())
            {
                IEnumerable<Project> projects = new List<Project>()
            {
                new Project()
                {
                    UserId = 1,
                    Description = "Projeto de Marketing"
                },

                new Project()
                {
                    UserId = 2,
                    Description = "Projeto de TI"
                },
            };

                context.Project.AddRange(projects);
                context.SaveChanges();
            }
        }

        public static void SeedTasks(TaskManagerContext context)
        {
            if (!context.Task.Any())
            {
                IEnumerable<Task.Manager.Domain.Entities.Task> tasks = new List<Task.Manager.Domain.Entities.Task>()
            {
                new Task.Manager.Domain.Entities.Task()
                {
                    Title = "Criar módulo de RH",
                    Description = "Aqui deve ser criado o módulo de RH onde terá como cadastrar funcionários, cargos e salários",
                    Priority = PriorityTask.High,
                    ProjectId = 1,
                    Status = StatusTask.Proposed,
                    ExpiredDate = DateTime.Now.AddMonths(1),
                },
                new Task.Manager.Domain.Entities.Task()
                {
                    Title = "Criar módulo de Contabilidade",
                    Description = "Aqui deve ser criado o módulo de Contabilidade onde terá como cadastrar contas contábeis e extrair relatórios",
                    Priority = PriorityTask.Medium,
                    ProjectId = 1,
                    Status = StatusTask.Active,
                    ExpiredDate = DateTime.Now.AddMonths(1),
                },
                new Task.Manager.Domain.Entities.Task()
                {
                    Title = "Criar módulo de Diretoria",
                    Description = "Aqui deve ser criado o módulo de Diretoria onde terá como emitir relatórios e vizualizar indicadores",
                    Priority = PriorityTask.Low,
                    ProjectId = 1,
                    Status = StatusTask.Closed,
                    ExpiredDate = DateTime.Now.AddMonths(-1),
                },
            };

                context.Task.AddRange(tasks);
                context.SaveChanges();
            }
        }
    }
}
