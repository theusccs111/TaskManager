using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Manager.Domain.Enums;
using Task.Manager.Domain.Extensions;

namespace Task.Manager.Domain.Resource.Response
{
    public class ReportPerformanceResponse
    {
        public long UserId { get; set; }
        public string Login { get; set; }
        public RoleUser Role { get; set; }
        public string RoleDescription { get { return Role.GetDescription(); } }
        public decimal AverageTasksCompleted { get; set; }
    }
}
