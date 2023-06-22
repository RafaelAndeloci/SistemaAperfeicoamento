using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAP_1.Models;
using SAP_1.Services.Interfaces;

namespace SAP_1.Controllers
{
    public class CursoOferecidoController : Controller
    {
        private ICursoOferecidoService _service;
        private IEmpregadoService _empregadoService;
        private List<SelectListItem> _status = new()
        {
            new SelectListItem() { Value = "true", Text = "Ativo"},
            new SelectListItem() { Value = "false", Text = "Desativado" }
        };
        private List<SelectListItem> GetListaDeInstrutor()
        {
            return _empregadoService.FindAll()
                .Select(g => new SelectListItem()
                {
                    Value = g.IdEmpregado.ToString(),
                    Text = g.NmEmpregado.ToString()
                }).ToList();
        }

        public CursoOferecidoController(
            ICursoOferecidoService service,
            IEmpregadoService empregadoService)
        {
            _service = service;
            _empregadoService = empregadoService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<CursoOferecido> cursos = _service.FindAll().ToList();
            return View(cursos);
        }

        [HttpGet]
        public IActionResult Create(string idCurso)
        {
            ViewBag.StatusCurso = _status;
            ViewBag.ListaInstrutor = GetListaDeInstrutor();
            return View(new CursoOferecido{ IdCurso = idCurso });
        }

        [HttpPost]
        public IActionResult Create(CursoOferecido curso)
        {
            _service.Create(curso);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Remove(string idCurso, DateTime dtInicio)
        {
            CursoOferecido curso = _service.Find(new CursoOferecido { IdCurso = idCurso, DtInicio = dtInicio });
            return View(curso);
        }

        [HttpPost]
        public IActionResult Remove(CursoOferecido curso)
        {
            _service.Delete(curso);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(string idCurso, string dtInicio)
        {
            DateTime data = DateTime.Parse(dtInicio);
            ViewBag.StatusCurso = _status;
            CursoOferecido curso = _service.Find(new CursoOferecido { IdCurso = idCurso, DtInicio = data });
            return View(curso);
        }

        [HttpPost]
        public IActionResult Editar(CursoOferecido curso, string idCurso, string dtAntigo)
        {
            DateTime data = DateTime.Parse(dtAntigo);
            CursoOferecido cursoARemover = _service.Find(new CursoOferecido { IdCurso = idCurso, DtInicio = data });


            _service.Delete(cursoARemover);
            _service.Create(curso);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Matricula(string idCurso, string dtInicio)
        {
            DateTime data = DateTime.Parse(dtInicio);
            CursoOferecido curso = _service.Find(new CursoOferecido { IdCurso = idCurso, DtInicio = data });

            return RedirectToAction("Index", "Matricula", curso);
        }
        
    }
}
