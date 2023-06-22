using Microsoft.EntityFrameworkCore;
using SAP_1.Models;
using SAP_1.Services.Interfaces;

namespace SAP_1.Services
{
    public class DbHistoricoContext : IHistoricoService
    {
        private AcademicoContext _context;

        public DbHistoricoContext(AcademicoContext context)
        {
            _context = context;
        }

        public void Create(Historico obj)
        {
            _context.TbHistoricos.Add(obj);
            _context.SaveChanges();
        }

        public void Update(Historico obj)
        {
            _context.TbHistoricos.Update(obj);
            _context.SaveChanges();
        }

        public void Delete(Historico obj)
        {
            _context.TbHistoricos.Remove(obj);
            _context.SaveChanges();
        }

        public ICollection<Historico> FindAll()
        {
            return _context.TbHistoricos.ToList();
        }

        public Historico? Find(Historico obj)
        {
            return _context.TbHistoricos.FirstOrDefault(h =>
                h.IdEmpregado == obj.IdEmpregado && h.DtInicio == obj.DtInicio);
        }

        public ICollection<Historico> MostrarTodasMovimentacoes(Empregado emp)
        {
            return _context.TbHistoricos.Where(h => h.IdEmpregado == emp.IdEmpregado).ToList();
        }

        public ICollection<Historico> FindAllDistinct()
        {
            var hist = _context.TbHistoricos;
            return hist.GroupBy(h => h.IdEmpregado).Select(g => g.First()).ToList();
        }
    }
}
