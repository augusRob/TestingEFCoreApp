﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestingEFCoreApp.Models;
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
        public ActionResult<List<MateriaDTO>> GetMaterias()
        {
            try
            {
                var materias = _materiaRepository.GetAll();
                var materiasDTO = materias.Select(m => m.ToDTO()).ToList();

                return Ok(materiasDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<MateriaDTO> GetMateriaById(int id)
        {
            try
            {
                var materia = _materiaRepository.GetById(id);
                if (materia == null)
                {
                    return NotFound();
                }
                
                return Ok(materia.ToDTO());  
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }


    }
}
