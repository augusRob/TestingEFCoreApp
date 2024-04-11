using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestingEFCoreApp.Repositories;

namespace TestingEFCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriasController : ControllerBase
    {
        private readonly IMateriaRepository _materiaRepository;

        public MateriasController(IMateriaRepository materiaRepository)
        {
            _materiaRepository = materiaRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetMaterias()
        {
            try
            {
                var materias = await _materiaRepository.GetAllAsync();
                return Ok(materias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetMateriaById(int id)
        {
            try
            {
                var materia = await _materiaRepository.GetByIdAsync(id);
                if (materia == null)
                {
                    return NotFound();
                }
                return Ok(materia);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
    }
}
