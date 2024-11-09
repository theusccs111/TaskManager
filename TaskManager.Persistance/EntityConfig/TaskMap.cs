using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Entities;

namespace TaskManager.Persistance.EntityConfig
{
    public class TaskMap : EntityConfigurationBase<Task.Manager.Domain.Entities.Task>
    {
        public override void Configure(EntityTypeBuilder<Task.Manager.Domain.Entities.Task> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Description).HasMaxLength(500);
        }
    }
}
