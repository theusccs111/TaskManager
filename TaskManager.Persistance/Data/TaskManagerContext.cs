using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Entities;
using Task.Manager.Domain.Entities.Base;
using Task.Manager.Domain.Enums;
using TaskManager.Persistance.EntityConfig;
using static System.Net.Mime.MediaTypeNames;

namespace TaskManager.Persistance.Data
{
    public class TaskManagerContext : DbContext
    {
        public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Task.Manager.Domain.Entities.Task> Task { get; set; }
        public DbSet<TaskAudit> TaskAudit { get; set; }

        public override int SaveChanges()
        {
            OnBeforeSaving();
            int saveChanes = base.SaveChanges();

            return saveChanes;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();

            var saveChanges = base.SaveChanges(acceptAllChangesOnSuccess);

            return saveChanges;
        }
        protected virtual void OnBeforeSaving()
        {
            var now = DateTime.Now.ToUniversalTime();
            ChangeTracker.DetectChanges();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Entity entity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.DateCreated = now;
                        entity.DateModified = now;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entity.DateModified = now;
                    }
                }
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Replace table names
                entity.SetTableName(entity.GetTableName()?.ToUpper());

                if (entity.GetProperties().Where(p => p.PropertyInfo != null).Any())
                {
                    entity.GetProperties().Where(p => p.PropertyInfo != null && p.PropertyInfo.PropertyType.Name.Equals("String")).ToList().ForEach(p => p.SetMaxLength(100));
                }

            }

            //Exemplo de map
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ProjectMap());
            modelBuilder.ApplyConfiguration(new TaskMap());
            modelBuilder.ApplyConfiguration(new TaskAuditMap());

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging();
#endif
        }

        public IDbContextTransaction BeginTransaction()
        {
            return Database.BeginTransaction();
        }
    }
}
