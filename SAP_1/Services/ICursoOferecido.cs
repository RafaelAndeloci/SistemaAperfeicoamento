using SAP_1.Models;

namespace SAP_1.Services
{
    public interface ICursoOferecido : IService<CursosOferecido>
    {
        public Empregado FindInstrutor(CursosOferecido curso);
    }
}
