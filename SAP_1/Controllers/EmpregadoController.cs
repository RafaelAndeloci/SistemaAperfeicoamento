using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAP_1.Models;
using SAP_1.Services;

namespace SAP_1.Controllers
{
    public class EmpregadoController : Controller
    {
        private IEmpregadoService _service;

        private List<SelectListItem> _status = new()
        {
            new SelectListItem() { Value = "true", Text = "Ativo"},
            new SelectListItem() { Value = "false", Text = "Desativado" }
        };

    public EmpregadoController(IEmpregadoService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            ICollection<Empregado> empregados = _service.FindAll();
            return View(empregados);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Empregado empregado)
        {
            if (empregado.IdGerente == empregado.IdEmpregado)
            {
                ModelState.AddModelError("IdGerente", "IdGerente não pode ser igual ao IdEmpregado.");
                return View(empregado);
            }
            _service.Create(empregado);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Remover(int idEmpregado)
        {
            Empregado empregado = _service.Find(new Empregado { IdEmpregado = idEmpregado });
            return View(empregado);
        }

        [HttpPost]
        public IActionResult Remover(Empregado empregado)
        {
            try
            {
                _service.Delete(empregado);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("IdEmpregado", "O empregado não pode ser removido pois ele é gerente de um departamento.");
                
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int idEmpregado)
        {
            ViewBag.StatusEmpregado = _status;
            Empregado empregado = _service.Find(new Empregado { IdEmpregado = idEmpregado });
            return View(empregado);
        }

        [HttpPost]
        public IActionResult Editar(Empregado empregado)
        {
            _service.Update(empregado);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Detalhar(int idEmpregado)
        {
            Empregado empregado = _service.Find(new Empregado { IdEmpregado = idEmpregado });
            return View(empregado);
        }
    }
}
