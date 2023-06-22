using SAP_1.Models;

namespace SAP_1.Services.Interfaces
{
    public interface ICursoOferecidoService : IService<CursoOferecido>
    {
        public Empregado FindInstrutor(CursoOferecido curso);
        public ICollection<Empregado> FindMatriculados(CursoOferecido curso);
    }
}
