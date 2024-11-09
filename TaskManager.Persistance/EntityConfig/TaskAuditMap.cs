using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Entities;

namespace TaskManager.Persistance.EntityConfig
{
    public class TaskAuditMap : EntityConfigurationBase<TaskAudit>
    {
        public override void Configure(EntityTypeBuilder<TaskAudit> builder)
        {
            base.Configure(builder);

            builder
            .HasOne(ta => ta.User)
            .WithMany(u => u.TaskAudits)
            .HasForeignKey(ta => ta.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
