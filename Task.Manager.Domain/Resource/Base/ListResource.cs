using System.Collections.Generic;

namespace Task.Manager.Domain.Resource.Base
{
    public class ListResource<T>
    {
        public IEnumerable<T> List { get; set; }
        public long TotalRecords { get; set; }
    }
}
