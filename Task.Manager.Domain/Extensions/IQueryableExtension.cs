using System.Linq;

namespace Task.Manager.Domain.Extensions
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> dados, int pagina, int quantidadeRegistros)
        {
            if (pagina == 0 || quantidadeRegistros == 0)
            {
                return dados;
            }
            else
            {
                return dados.Skip((pagina - 1) * quantidadeRegistros)
                    .Take(quantidadeRegistros);
            }
        }
    }
}
