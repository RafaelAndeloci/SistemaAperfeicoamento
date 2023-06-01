using SAP_1.Models;

namespace SAP_1.Services
{
    public interface IEmpregadoService : IService<Empregado>
    {
        public ICollection<Empregado> FindSubordinados(Empregado gerente);
    }
}
