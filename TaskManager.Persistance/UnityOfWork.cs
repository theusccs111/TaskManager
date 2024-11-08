using System;
using System.Collections.Generic;
using Task.Manager.Domain.Entities;
using Task.Manager.Domain.Entities.Base;
using TaskManager.Persistance.Data;
using TaskManager.Persistance.Repositories;
using TaskManager.Service.Interface.Persistance;
using TaskManager.Service.Interface.Persistance.Repository;

namespace SOMA.OPEX.Persistance
{
    public class UnityOfWork : IUnityOfWork, IDisposable
    {
        private readonly TaskManagerContext _context;
        private Dictionary<string, object> repositories;

        public IRepository<User> User { get { return new Repository<User>(_context); } }
        public IRepository<Project> Project { get { return new Repository<Project>(_context); } }
        public IRepository<Task.Manager.Domain.Entities.Task> Task { get { return new Repository<Task.Manager.Domain.Entities.Task>(_context); } }
        public IRepository<TaskAudit> TaskAudit { get { return new Repository<TaskAudit>(_context); } }
        
        public UnityOfWork(TaskManagerContext context)
        {
            _context = context;
        }
        public async System.Threading.Tasks.Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SetAutoDetectChanges(bool isDetectChanges)
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = isDetectChanges;
        }

        public IRepository<T> Repository<T>() where T : Entity
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;
            if (!repositories.ContainsKey(type))
            {
                var repositorioType = typeof(Repository<>);
                var repositorioInstancia = Activator.CreateInstance(repositorioType.MakeGenericType(typeof(T)), _context);
                repositories.Add(type, repositorioInstancia);
            }

            return (Repository<T>)repositories[type];
        }
    }
}
