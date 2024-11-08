using System.ComponentModel;

namespace Task.Manager.Domain.Enums
{
    public enum StatusTask
    {
        [Description("Pendente")]
        Proposed = 0,
        [Description("Andamento")]
        Active = 1,
        [Description("Concluída")]
        Closed = 2

    }
}
