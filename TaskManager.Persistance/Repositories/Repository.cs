using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using Task.Manager.Domain.Entities.Base;
using TaskManager.Persistance.Data;
using TaskManager.Service.Interface.Persistance.Repository;

namespace TaskManager.Persistance.Repositories
{
    public class Repository<T> : IRepository<T>, IDisposable where T : Entity
    {
        private readonly TaskManagerContext _context;

        public Repository(TaskManagerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obter todos os dados
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }


        /// <summary>
        /// Obter todos os dados com include
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetAllLazyLoad(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = GetAll().Where(predicate);
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        /// <summary>
        /// Obter dbSet
        /// </summary>
        /// <returns></returns>
        public DbSet<T> GetDbSet()
        {
            return _context.Set<T>();
        }

        /// <summary>
        /// Obter dados filtrados por expressão lambda
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public T Search(params object[] key)
        {
            return _context.Set<T>().Find(key);
        }

        /// <summary>
        /// Obter primeiro registro conforme expressão lambda
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T GetFirst(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Func<T, bool> predicate)
        {
            _context.Set<T>().Where(predicate).ToList().ForEach(del => Delete(del));
        }

        public void Delete(T obj)
        {
            var entity = _context.Set<T>().Find(obj.Id);
            _context.Set<T>().Remove(entity);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
