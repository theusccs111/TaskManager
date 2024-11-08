using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using Task.Manager.Domain.Entities.Base;

namespace TaskManager.Service.Interface.Persistance.Repository
{
    public interface IRepository<T> where T : Entity
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAllLazyLoad(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        DbSet<T> GetDbSet();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        T Search(params object[] key);
        T GetFirst(Expression<Func<T, bool>> predicate);
        void Create(T entity);
        void Update(T entity);
        void Delete(Func<T, bool> predicate);
        void Delete(T obj);
        void Commit();
        void Dispose();
    }
}
