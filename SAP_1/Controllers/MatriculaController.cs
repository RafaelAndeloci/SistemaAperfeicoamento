using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAP_1.Models;
using SAP_1.Services.Interfaces;

namespace SAP_1.Controllers
{
    public class MatriculaController : Controller
    {
        private IMatriculaService _service;
        private IEmpregadoService _empregadoService;
        private ICursoOferecido _cursoService;

        private List<SelectListItem> _status = new()
        {
            new SelectListItem() { Value = "true", Text = "Ativo"},
            new SelectListItem() { Value = "false", Text = "Desativado" }
        };

        private List<SelectListItem> GetListIdCurso()
        {
            return _cursoService.FindAll()
                .Select(c => new SelectListItem()
                {
                    Value = c.IdCurso.ToString(),
                    Text = c.IdCurso.ToString()
                }).ToList();
        }

        private List<SelectListItem> GetListDtCurso()
        {
            return _cursoService.FindAll()
                .Select(c => new SelectListItem()
                {
                    Value = c.DtInicio.ToString(),
                    Text = c.DtInicio.ToString()
                }).ToList();
        }

        private List<SelectListItem> GetListaPessoas()
        {
            return _empregadoService.FindAll()
                .Select(g => new SelectListItem()
                {
                    Value = g.IdEmpregado.ToString(),
                    Text = g.NmEmpregado.ToString()
                }).ToList();
        }

        public MatriculaController(
            IMatriculaService service,
            IEmpregadoService empregadoService,
            ICursoOferecido cursoService)
        {
            _service = service;
            _empregadoService = empregadoService;
            _cursoService = cursoService;
        }

        public IActionResult Index()
        {
            List<Matricula> matriculas = _service.FindAll().ToList();
            return View(matriculas);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            ViewBag.ListaPessoas = GetListaPessoas();
            ViewBag.ListaIdCurso = GetListIdCurso();
            ViewBag.ListaDtCurso = GetListDtCurso();
            ViewBag.Status = _status;
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Matricula matricula)
        {
            _service.Create(matricula);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(string idCurso, string dtInicio, int idParticipante)
        {
            ViewBag.Status = _status;
            ViewBag.ListaIdCurso = GetListIdCurso();
            ViewBag.ListaDtCurso = GetListDtCurso();

            DateTime data = DateTime.Parse(dtInicio);
            Matricula matricula = _service.Find(new Matricula
                { IdCurso = idCurso, DtInicio = data, IdParticipante = idParticipante });
            return View(matricula);
        }

        [HttpPost]
        public IActionResult Editar(Matricula matricula, string dtAntigo, string idCurso, int idParticipante)
        {
            DateTime data = DateTime.Parse(dtAntigo);
            Matricula matriculaRemover = _service.Find(new Matricula
                { IdCurso = idCurso, DtInicio = data, IdParticipante = idParticipante });

            _service.Delete(matriculaRemover);
            _service.Create(matricula);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Remover(string idCurso, string dtInicio, int idParticipante)
        {
            DateTime data = DateTime.Parse(dtInicio);
            Matricula matricula = _service.Find(new Matricula
                { IdCurso = idCurso, DtInicio = data, IdParticipante = idParticipante });
            return View(matricula);
        }

        [HttpPost]
        public IActionResult Remover(Matricula matricula)
        {
            _service.Delete(matricula);
            return RedirectToAction("Index");
        }
    }
}
