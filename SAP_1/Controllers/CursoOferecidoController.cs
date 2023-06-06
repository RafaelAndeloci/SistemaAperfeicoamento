using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAP_1.Models;
using SAP_1.Services;

namespace SAP_1.Controllers
{
    public class CursoOferecidoController : Controller
    {
        private ICursoOferecido _service;
        private ICursoService _cursoService;
        private List<SelectListItem> _status = new()
        {
            new SelectListItem() { Value = "true", Text = "Ativo"},
            new SelectListItem() { Value = "false", Text = "Desativado" }
        };


        public CursoOferecidoController(ICursoOferecido service, ICursoService cursoService)
        {
            _service = service;
            _cursoService = cursoService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<CursosOferecido> cursos = _service.FindAll().ToList();
            return View(cursos);
        }

        [HttpGet]
        public IActionResult Create(string idCurso)
        {
            ViewBag.StatusCurso = _status;
            return View(new CursosOferecido{ IdCurso = idCurso });
        }

        [HttpPost]
        public IActionResult Create(CursosOferecido curso)
        {
            _service.Create(curso);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Remove(string idCurso, DateTime dtInicio)
        {
            CursosOferecido curso = _service.Find(new CursosOferecido { IdCurso = idCurso, DtInicio = dtInicio });
            return View(curso);
        }

        [HttpPost]
        public IActionResult Remove(CursosOferecido curso)
        {
            _service.Delete(curso);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(string idCurso, DateTime dtInicio)
        {
            ViewBag.StatusCurso = _status;
            CursosOferecido curso = _service.Find(new CursosOferecido { IdCurso = idCurso, DtInicio = dtInicio });
            return View(curso);
        }

        [HttpPost]
        public IActionResult Editar(CursosOferecido curso)
        {
            _service.Update(curso);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Detalhar(string idCurso, DateTime dtInicio)
        {
            CursosOferecido curso = _service.Find(new CursosOferecido { IdCurso = idCurso, DtInicio = dtInicio });
            return View(curso);
        }
    }
}
