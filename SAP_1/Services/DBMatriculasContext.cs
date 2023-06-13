using SAP_1.Models;
using SAP_1.Services.Interfaces;

namespace SAP_1.Services
{
    public class DBMatriculasContext : IMatriculaService
    {
        private AcademicoContext _context;

        public DBMatriculasContext(AcademicoContext context)
        {
            _context = context;
        }

        public void Create(Matricula matricula)
        {
            _context.TbMatriculas.Add(matricula);
            _context.SaveChanges();
        }

        public void Update(Matricula matricula)
        {
            _context.TbMatriculas.Update(matricula);
            _context.SaveChanges();
        }

        public void Delete(Matricula matricula)
        {
            _context.TbMatriculas.Remove(matricula);
            _context.SaveChanges();
        }

        public ICollection<Matricula> FindAll()
        {
            return _context.TbMatriculas.ToList();
        }

        public Matricula? Find(Matricula matricula)
        {
            return _context.TbMatriculas
                .FirstOrDefault(m =>
                    m.IdParticipante == matricula.IdParticipante &&
                    m.IdCurso == matricula.IdCurso &&
                    m.DtInicio == matricula.DtInicio);
        }
    }
}
