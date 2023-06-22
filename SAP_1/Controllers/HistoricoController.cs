using Microsoft.AspNetCore.Mvc;
using SAP_1.Models;
using SAP_1.Services.Interfaces;

namespace SAP_1.Controllers
{
    public class HistoricoController : Controller
    {
        private IHistoricoService _service;
        private IEmpregadoService _empregadoService;

        public HistoricoController(IHistoricoService service, IEmpregadoService empregadoService)
        {
            _service = service;
            _empregadoService = empregadoService;
        }

        public IActionResult Index()
        {
            var historicos =  _service.FindAllDistinct();
            return View(historicos);
        }

        public IActionResult Detalhes(int idEmpregado, string dtInicio)
        {
            Empregado emp = _empregadoService.Find(new Empregado { IdEmpregado = idEmpregado });

            List<Historico> hists = _service.MostrarTodasMovimentacoes(emp).ToList();
            return View(hists);
        }

    }
}
