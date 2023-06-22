using SAP_1.Models;

namespace SAP_1.Services.Interfaces
{
    public interface IHistoricoService : IService<Historico>
    {
        ICollection<Historico> MostrarTodasMovimentacoes(Empregado emp);
        ICollection<Historico> FindAllDistinct();
    }
}
