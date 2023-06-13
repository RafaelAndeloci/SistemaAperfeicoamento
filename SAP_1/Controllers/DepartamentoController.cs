using Microsoft.AspNetCore.Mvc;
using SAP_1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAP_1.Services.Interfaces;

namespace SAP_1.Controllers
{
    public class DepartamentoController : Controller
    {
        private IDepartamentoService _service;
        private IEmpregadoService _empregadoService;

        private List<SelectListItem> GetGerentesListItem()
        {
            return _empregadoService.FindGerentes()
                .Select(g => new SelectListItem()
                {
                    Value = g.IdEmpregado.ToString(),
                    Text = g.NmEmpregado.ToString()
                }).ToList();
        }

        public DepartamentoController(IDepartamentoService service, IEmpregadoService empregadoService)
        {
            _service = service;
            _empregadoService = empregadoService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Departamento> todosDepartamentos = _service.FindAll().ToList();
            return View(todosDepartamentos);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            ViewBag.ListaGerentes = GetGerentesListItem();

            return View();
        }

        [HttpPost]
        public IActionResult Criar(Departamento departamento)
        {
            _service.Create(departamento);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int idDepartamento)
        {
            Departamento departamento = _service.Find(new Departamento{ IdDepartamento = idDepartamento});
            return View(departamento);
        }

        [HttpPost]
        public IActionResult Editar(Departamento departamento)
        {
            _service.Update(departamento);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Remover(int idDepartamento)
        {
            Departamento departamento = _service.Find(new Departamento { IdDepartamento = idDepartamento });
            return View(departamento);
        }

        [HttpPost]
        public IActionResult Remover(Departamento departamento)
        {
            _service.Delete(departamento);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Detalhar(int idDepartamento)
        {
            Departamento depto = _service.Find(new Departamento { IdDepartamento = idDepartamento });
            List<Empregado> empregados = _service.FindEmpregados(depto).ToList();

            return View((depto, empregados));
        }
    }
}
