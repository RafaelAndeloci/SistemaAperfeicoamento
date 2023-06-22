using SAP_1.Models;
using SAP_1.Services.Interfaces;

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
            _context.TbHistoricos.Add(new Historico { IdEmpregado = empregado.IdEmpregado, DtInicio = DateTime.Now, IdDepartamento = empregado.IdDepartamento.Value });
            _context.SaveChanges();
        }

        public void Update(Empregado obj)
        {
            _context.TbEmpregados.Update(obj);
            _context.SaveChanges();
        }
        public void Update(Empregado empregado, string comentarios)
        {
            Historico hist = PreencherHistorico(empregado, comentarios);
            _context.TbHistoricos.Add(hist);
            _context.TbEmpregados.Update(empregado);
            _context.SaveChanges();
        }
        public void Delete(Empregado empregado)
        {
            var curso = FindCursoOferecido(empregado).ToList();
            var sub = FindSubordinados(empregado).ToList();
            var dep = FindDeptoSubordinados(empregado).ToList();
            var mat = FindMatricula(empregado).ToList();

            if (sub != null)
            {
                foreach (var subordinado in sub)
                {
                    subordinado.IdGerente = null;
                }
            }
            if (dep != null)
            {
                foreach (var departamento in dep)
                {
                    departamento.IdGerente = null;
                }
            }
            if (curso != null)
            {
                foreach (CursoOferecido cursin in curso)
                {
                    cursin.IdInstrutor = null;
                }
            }
            if (mat != null)
            {
                foreach (var matricula in mat)
                {
                    _context.TbMatriculas.Remove(matricula);
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
        public ICollection<Empregado> FindGerentes()
        {
            List<Empregado> empregados = FindAll().ToList();

            List<int> ids = new List<int>();

            foreach (var empregado in empregados)
            {
                if (empregado.IdGerente.HasValue) ids.Add(empregado.IdGerente.Value);
                
            }
            return _context.TbEmpregados.Where(e => ids.Contains(e.IdEmpregado)).ToList();
        }
        public ICollection<CursoOferecido> FindCursoOferecido(Empregado instrutor)
        {
            return _context.TbCursosOferecidos.Where(c => c.IdInstrutor == instrutor.IdEmpregado).ToList();
        }
        public ICollection<Matricula> FindMatricula(Empregado estudante)
        {
            return _context.TbMatriculas.Where(m => m.IdParticipante == estudante.IdEmpregado).ToList();
        }
        public ICollection<Departamento> FindDeptoSubordinados(Empregado gerente)
        {
            return _context.TbDepartamentos
                .Where(d => d.IdGerente == gerente.IdEmpregado)
                .ToList();

        }
        public Empregado? Find(Empregado empregado)
        {
            return _context.TbEmpregados.FirstOrDefault(e => 
                e.IdEmpregado == empregado.IdEmpregado);
        }
        public Historico PreencherHistorico(Empregado empregado, string comentarios)
        {
            var hist = new Historico();
            hist.IdEmpregado = empregado.IdEmpregado;
            hist.DtInicio = DateTime.Now;
            hist.IdDepartamento = empregado.IdDepartamento.Value;
            hist.AnoInicio = DateTime.Now.Year;
            hist.Comentarios = comentarios;
            hist.FgAtivo = empregado.FgAtivo;

            return hist;
        }
    }
}
