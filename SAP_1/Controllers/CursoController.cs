using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAP_1.Models;
using SAP_1.Services.Interfaces;

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

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult GetCursos()
    {
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sort = Request.Form["sort"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            var cursos = _service.FindAll();
            if (!string.IsNullOrEmpty(searchValue))
            {
                cursos = cursos.Where(c => c.DsCurso.Contains(searchValue)).ToList();
            }
            recordsTotal = cursos.Count();
            var data = cursos.Skip(skip).Take(pageSize).Select(c => new {
                idCurso = c.IdCurso,
                dsCurso = c.DsCurso,
                categoria = c.Categoria,
                duracao = c.Duracao,
                ativo = c.FgAtivo
            }).ToList();

            var jsonData = new 
            { 
                draw = draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = data 
            };
            return Json(jsonData);
        }
        catch (System.Exception)
        {
            throw;
        }
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
            if (curso != null)
            {
                _service.Create(curso);
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Remover(string idCurso)
        {
            Curso curso = _service.Find(new Curso { IdCurso = idCurso });

            return curso == null ? NotFound() : View(curso);
        }

        [HttpPost]
        public IActionResult Remover(Curso curso)
        {
            if (curso != null)
            {
                _service.Delete(curso);
                return RedirectToAction("Index");
            }
            return NotFound();

        }

        [HttpGet]
        public IActionResult Detalhar(string idCurso)
        {
            Curso curso = _service.Find(new Curso { IdCurso = idCurso });

            return curso == null ? NotFound() : View(curso);
        }

        [HttpGet]
        public IActionResult Editar(string idCurso)
        {
            ViewBag.CategoriaCurso = _categorias;
            ViewBag.StatusCurso = _status;
            Curso curso = _service.Find(new Curso { IdCurso = idCurso });

            return curso == null ? NotFound() : View(curso);
        }

        [HttpPost]
        public IActionResult Editar(Curso curso)
        {
            _service.Update(curso);
            return RedirectToAction("Index");
        }
    }
}
