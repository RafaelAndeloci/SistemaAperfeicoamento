using SAP_1.Models;

namespace SAP_1.Services
{
    public class DBDepartamentosContext : IDepartamentoService
    {
        private AcademicoContext _context;

        public DBDepartamentosContext(AcademicoContext context)
        {
            _context = context;
        }

        public void Create(Departamento empregado)
        {
            _context.TbDepartamentos.Add(empregado);
            _context.SaveChanges();
        }

        public void Delete(Departamento departamento)
        {
            _context.TbDepartamentos.Remove(departamento);
            _context.SaveChanges();
        }

        public Departamento? Find(Departamento departamento)
        {
            return _context.TbDepartamentos.FirstOrDefault(d =>
                d.IdDepartamento == departamento.IdDepartamento);
        }

        public ICollection<Departamento> FindAll()
        {
            return _context.TbDepartamentos.ToList();
        }

        public void Update(Departamento departamento)
        {
            _context.TbDepartamentos.Update(departamento);
            _context.SaveChanges();
        }

        public ICollection<Empregado> FindEmpregados(Departamento departamento)
        {
            return _context.TbEmpregados
                .Where(e => e.IdDepartamento == departamento.IdDepartamento)
                .ToList();
        }

    }
}
