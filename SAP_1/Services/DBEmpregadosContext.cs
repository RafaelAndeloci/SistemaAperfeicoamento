using SAP_1.Models;

namespace SAP_1.Services
{
    public class DBEmpregadosContext : IEmpregadoService
    {
        private AcademicoContext _context;

        public DBEmpregadosContext(AcademicoContext context)
        {
            _context = context;
        }

        public void Create(Empregado empregado)
        {
            _context.TbEmpregados.Add(empregado);
            _context.SaveChanges();
        }

        public void Update(Empregado empregado)
        {
            _context.TbEmpregados.Update(empregado);
            _context.SaveChanges();
        }

        public void Delete(Empregado empregado)
        {
            if (FindSubordinados(empregado) != null)
            {
                List<Empregado> sub = FindSubordinados(empregado).ToList();
                foreach (var subordinado in sub)
                {
                    subordinado.IdGerente = null;
                }
            }
                _context.TbEmpregados.Remove(empregado);
            _context.SaveChanges();
        }

        public ICollection<Empregado> FindAll()
        {
            return _context.TbEmpregados.ToList();
        }
        
        public ICollection<Empregado> FindSubordinados(Empregado gerente)
        {
            return _context.TbEmpregados
                .Where(e => e.IdGerente == gerente.IdEmpregado)
                .ToList();
        }

        public Empregado? Find(Empregado empregado)
        {
            return _context.TbEmpregados.FirstOrDefault(e => 
                e.IdEmpregado == empregado.IdEmpregado);
        }
    }
}
