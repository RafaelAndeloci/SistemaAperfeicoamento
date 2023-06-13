using SAP_1.Models;
using SAP_1.Services.Interfaces;

namespace SAP_1.Services
{
    public class DbCursosContext : ICursoService
    {
        private AcademicoContext _context;

        public DbCursosContext(AcademicoContext context)
        {
            _context = context;
        }

        public void Create(Curso curso)
        {
            _context.TbCursos.Add(curso);
            _context.SaveChanges();
        }

        public void Update(Curso curso)
        {
            _context.TbCursos.Update(curso);
            _context.SaveChanges();
        }

        public void Delete(Curso curso)
        {
            _context.TbCursos.Remove(curso);
            _context.SaveChanges();
        }

        public ICollection<Curso> FindAll()
        {
            return _context.TbCursos.ToList();
        }

        public Curso? Find(Curso curso)
        {
            return _context.TbCursos.FirstOrDefault(c => c.IdCurso == curso.IdCurso);
        }
    }
}
