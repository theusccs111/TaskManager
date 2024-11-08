using System;
using Task.Manager.Domain.Entities;
using Task.Manager.Domain.Entities.Base;
using TaskManager.Service.Interface.Persistance.Repository;

namespace TaskManager.Service.Interface.Persistance
{
    public interface IUnityOfWork : IDisposable
    {
        IRepository<User> User { get; }
        IRepository<Project> Project { get; }
        IRepository<Task.Manager.Domain.Entities.Task> Task { get; }
        IRepository<TaskAudit> TaskAudit { get; }
        System.Threading.Tasks.Task CompleteAsync();
        void Complete();
        void SetAutoDetectChanges(bool isDetectChanges);
        IRepository<T> Repository<T>() where T : Entity;
    }
}
