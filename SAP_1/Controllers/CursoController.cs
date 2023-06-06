using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAP_1.Models;
using SAP_1.Services;

namespace SAP_1.Controllers
{
    public class CursoController : Controller
    {
        private ICursoService _service;
        private List<SelectListItem> _status = new()
        {
            new SelectListItem() { Value = "true", Text = "Ativo"},
            new SelectListItem() { Value = "false", Text = "Desativado" }
        };

        private List<SelectListItem> _categorias = new()
        {
            new SelectListItem() { Value = "GEN", Text = "GEN"},
            new SelectListItem() { Value = "BLD", Text = "BLD"},
            new SelectListItem() { Value = "DSG", Text = "DSG"}
        };



    public CursoController(ICursoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Curso> cursos = _service.FindAll().ToList();
            return View(cursos);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            ViewBag.StatusCurso = _status;
            ViewBag.CategoriaCurso = _categorias;
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Curso curso)
        {
            _service.Create(curso);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Remover(string idCurso)
        {
            Curso curso = _service.Find(new Curso { IdCurso = idCurso });
            return View(curso);
        }

        [HttpPost]
        public IActionResult Remover(Curso curso)
        {
            _service.Delete(curso);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Detalhar(string idCurso)
        {
            Curso curso = _service.Find(new Curso { IdCurso = idCurso });
            return View(curso);
        }

        [HttpGet]
        public IActionResult Editar(string idCurso)
        {
            ViewBag.CategoriaCurso = _categorias;
            ViewBag.StatusCurso = _status;
            Curso curso = _service.Find(new Curso { IdCurso = idCurso });
            return View(curso);
        }

        [HttpPost]
        public IActionResult Editar(Curso curso)
        {
            _service.Update(curso);
            return RedirectToAction("Index");
        }
    }
}
