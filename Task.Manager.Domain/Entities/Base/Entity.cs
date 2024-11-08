using System;

namespace Task.Manager.Domain.Entities.Base
{
    public class Entity
    {
        public long Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
