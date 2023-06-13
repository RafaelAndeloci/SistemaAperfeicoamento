using SAP_1.Models;

namespace SAP_1.Services.Interfaces
{
    public interface IEmpregadoService : IService<Empregado>
    {
        public ICollection<Empregado> FindSubordinados(Empregado gerente);
        public ICollection<Empregado> FindGerentes();
        public ICollection<CursoOferecido> FindCursoOferecido(Empregado instrutor);
        public ICollection<Matricula> FindMatricula(Empregado estudante);
    }
}
