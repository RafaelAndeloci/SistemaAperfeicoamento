using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SAP_1.Models;
using SAP_1.Services.Interfaces;
using SAP_1.ViewModels;

namespace SAP_1.Controllers
{
    public class EmpregadoController : Controller
    {
        private IEmpregadoService _service;
        private IDepartamentoService _deptoService;

        private List<SelectListItem> GetEmpregadosList()
        {
            return _service.FindAll()
                .Select(g => new SelectListItem()
                {
                    Value = g.IdEmpregado.ToString(),
                    Text = g.NmEmpregado.ToString()
                }).ToList();
        }
        private List<SelectListItem> GetGerentesListItem()
        {
            return _service.FindGerentes()
                .Select(g => new SelectListItem()
                {
                    Value = g.IdEmpregado.ToString(),
                    Text = g.NmEmpregado.ToString()
                }).ToList();
        }
        private List<SelectListItem> GetDeptoListItem()
        {
            return _deptoService.FindAll()
                .Select(d => new SelectListItem()
                {
                    Value = d.IdDepartamento.ToString(),
                    Text = d.IdDepartamento.ToString()
                }).ToList();
        }
        private List<SelectListItem> _status = new()
        {
            new SelectListItem() { Value = "true", Text = "Ativo"},
            new SelectListItem() { Value = "false", Text = "Desativado" }
        };

    public EmpregadoController(IEmpregadoService service, IDepartamentoService deptoService)
        {
            _service = service;
            _deptoService = deptoService;
        }
        public IActionResult Index()
        {
            ICollection<Empregado> empregados = _service.FindAll();
            return View(empregados);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            ViewBag.ListaGerentes = GetGerentesListItem();
            ViewBag.ListaDepto = GetDeptoListItem();
            ViewBag.ListaEmpregados = GetEmpregadosList();
            ViewBag.ListDeptoObject = _deptoService.FindAll().ToList();
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
            _service.Delete(empregado);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int idEmpregado)
        {
            ViewBag.StatusEmpregado = _status;
            ViewBag.ListaDepto = GetDeptoListItem();
            Empregado empregado = _service.Find(new Empregado { IdEmpregado = idEmpregado });
            var empvm = new EmpregadoViewModel { Empregado = empregado, Comentario = string.Empty};
            return View(empvm);
        }

        [HttpPost]
        public IActionResult Editar([Bind("Empregado", "Comentario")] EmpregadoViewModel empvm)
        {
            string? comentarios = empvm.Comentario;
            Empregado emp = empvm.Empregado;
                _service.Update(emp, comentarios);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Detalhar(int idEmpregado)
        {
            Empregado empregado = _service.Find(new Empregado { IdEmpregado = idEmpregado });
            ViewBag.IsGerente = _service.FindSubordinados(empregado);
            return View(empregado);
        }
    }
}
