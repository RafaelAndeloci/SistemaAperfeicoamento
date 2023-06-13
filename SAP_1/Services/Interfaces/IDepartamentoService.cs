using SAP_1.Models;

namespace SAP_1.Services.Interfaces
{
    public interface IDepartamentoService : IService<Departamento>
    {
        public ICollection<Empregado> FindEmpregados(Departamento depto);
    }
}
