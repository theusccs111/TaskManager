using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Manager.Domain.Resource.Response
{
    public class ReportPerformanceResponse
    {
        public long UserId { get; set; }
        public int AverageTasksCompleted { get; set; }
    }
}
