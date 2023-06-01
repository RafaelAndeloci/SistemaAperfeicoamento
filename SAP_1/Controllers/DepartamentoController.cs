using Microsoft.AspNetCore.Mvc;
using SAP_1.Services;
using SAP_1.Models;

namespace SAP_1.Controllers
{
    public class DepartamentoController : Controller
    {
        private IDepartamentoService _service;

        public DepartamentoController(IDepartamentoService service)
        {
            _service = service;
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
