using SAP_1.Models;
using SAP_1.Services.Interfaces;

namespace SAP_1.Services
{
    public class DbCursosOferecidosContext : ICursoOferecidoService
    {
        private AcademicoContext _context;

        public DbCursosOferecidosContext(AcademicoContext context)
        {
            _context = context;
        }

        public void Create(CursoOferecido curso)
        {
            _context.TbCursosOferecidos.Add(curso);
            _context.SaveChanges();
        }

        public void Update(CursoOferecido curso)
        {
            _context.TbCursosOferecidos.Update(curso);
            _context.SaveChanges();
        }

        public void Delete(CursoOferecido curso)
        {
            _context.TbCursosOferecidos.Remove(curso);
            _context.SaveChanges();
        }

        public ICollection<CursoOferecido> FindAll()
        {
            return _context.TbCursosOferecidos.ToList();
        }

        public CursoOferecido? Find(CursoOferecido curso)
        {
            return _context.TbCursosOferecidos.FirstOrDefault(c =>
                c.IdCurso == curso.IdCurso && c.DtInicio == curso.DtInicio);
        }

        public Empregado FindInstrutor(CursoOferecido curso)
        {
            return _context.TbEmpregados.Where(e => e.IdEmpregado == curso.IdInstrutor).First();
        }

        public ICollection<Empregado> FindMatriculados(CursoOferecido curso)
        {
            List<Matricula> matriculas = _context.TbMatriculas
                .Where(m => 
                    m.IdCurso == curso.IdCurso && m.DtInicio == curso.DtInicio)
                .ToList();

            List<int> idParticipantes = new List<int>();

            foreach (var matricula in matriculas)
            {
                idParticipantes.Add(matricula.IdParticipante);
            }

            return _context.TbEmpregados.Where(e => idParticipantes.Contains(e.IdEmpregado)).ToList();
        }
    }
}
