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
                List<Task.Manager.Domain.Entities.Task> tasks = new List<Task.Manager.Domain.Entities.Task>();

                for (int i = 1; i <= 15; i++)
                {
                    tasks.Add(new Task.Manager.Domain.Entities.Task()
                    {
                        Title = $"Tarefa de Projeto 1 - {i}",
                        Description = $"Descrição para Tarefa {i} do Projeto 1",
                        Priority = (PriorityTask)(i % 3), 
                        ProjectId = 1,
                        UserId = i % 2 == 0 ? 1 : 2, 
                        Status = i % 3 == 0 ? StatusTask.Closed : StatusTask.Active, 
                        ExpiredDate = DateTime.Now.AddMonths(1),
                        ClosedDate = i % 3 == 0 ? DateTime.Now.AddDays(-i) : null 
                    });
                }


                for (int i = 1; i <= 10; i++)
                {
                    tasks.Add(new Task.Manager.Domain.Entities.Task()
                    {
                        Title = $"Tarefa de Projeto 2 - {i}",
                        Description = $"Descrição para Tarefa {i} do Projeto 2",
                        Priority = (PriorityTask)(i % 3), 
                        ProjectId = 2,
                        UserId = i % 2 == 0 ? 1 : 2, 
                        Status = i % 3 == 0 ? StatusTask.Closed : StatusTask.Proposed, 
                        ExpiredDate = DateTime.Now.AddMonths(1),
                        ClosedDate = i % 3 == 0 ? DateTime.Now.AddDays(-i) : null 
                    });
                }

                context.Task.AddRange(tasks);
                context.SaveChanges();
            }
        }


    }
}
