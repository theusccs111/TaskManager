using System.ComponentModel;

namespace Task.Manager.Domain.Enums
{
    public enum PriorityTask
    {
        [Description("Baixa")]
        Low = 0,
        [Description("Média")]
        Medium = 1,
        [Description("Alta")]
        High = 2

    }
}
