using SAP_1.Models;

namespace SAP_1.Services
{
    public class DbCursosOferecidosContext : ICursoOferecido
    {
        private AcademicoContext _context;

        public DbCursosOferecidosContext(AcademicoContext context)
        {
            _context = context;
        }

        public void Create(CursosOferecido curso)
        {
            _context.TbCursosOferecidos.Add(curso);
            _context.SaveChanges();
        }

        public void Update(CursosOferecido curso)
        {
            _context.TbCursosOferecidos.Update(curso);
            _context.SaveChanges();
        }

        public void Delete(CursosOferecido curso)
        {
            _context.TbCursosOferecidos.Remove(curso);
            _context.SaveChanges();
        }

        public ICollection<CursosOferecido> FindAll()
        {
            return _context.TbCursosOferecidos.ToList();
        }

        public CursosOferecido? Find(CursosOferecido curso)
        {
            return _context.TbCursosOferecidos.FirstOrDefault(c =>
                c.IdCurso == curso.IdCurso && c.DtInicio == curso.DtInicio);
        }

        public Empregado FindInstrutor(CursosOferecido curso)
        {
            return _context.TbEmpregados.Where(e => e.IdEmpregado == curso.IdInstrutor).First();
        }
    }
}
