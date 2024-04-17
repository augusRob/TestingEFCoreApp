using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using TestingEFCoreApp.Data;
using TestingEFCoreApp.Models;

namespace TestingEFCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriasController : ControllerBase
    {
        private readonly EFCoreAppContext _context;

        public MateriasController(EFCoreAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<MateriaDTO> GetMateriaByName(string name)
        {
            var materia = _context.Materia.FirstOrDefault(m => m.Nombre == name);
            return materia is null ? NotFound() : materia.ToDTO();
        }

        [HttpGet]
        public ActionResult<MateriaDTO> GetMateriaById(int id)
        {
            var materia = _context.Materia.FirstOrDefault(m => m.Id == id);
            return materia is null ? NotFound() : materia.ToDTO();
        }

        [HttpGet]
        public ActionResult<MateriaDTO[]> GetAllMaterias()
        {
            var materias = _context.Materia.OrderBy(m => m.Nombre);
            return materias.Select(m => m.ToDTO()).ToArray();
        }
            
    }
}
