using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CitaMedica.Models;
using CitaMedica.Repositories;
using AutoMapper;
using CitaMedica.Models.DTO;
using CitaMedica.Services;

namespace CitaMedica.Controllers
{
    [Route("citas")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        private readonly CMContext _context;
        private readonly IMapper _mapper;
        private readonly ICitaService _citaService;

        public CitasController(CMContext context, IMapper mapper, ICitaService citaService)
        {
            _context = context;
            _mapper = mapper;
            _citaService = citaService;
        }

        // GET: api/Citas
        [HttpGet]
        public ActionResult<IEnumerable<CitaDTO>> GetAllCitas()
        {
            IList<CitaDTO> citasDTO = new List<CitaDTO>();
            var citas = _citaService.GetAllCitas();
            foreach (Cita c in citas)
            {
                citasDTO.Add(_mapper.Map<CitaDTO>(c));
            }
            return citasDTO.ToList();
        }

        // GET: api/Citas/5
        [HttpGet("{id}")]
        public ActionResult<CitaDTO> GetCitaById(long id)
        {
            var cita = _citaService.GetCitaById(id);

            if (cita == null)
            {
                return BadRequest("Cita no encontrada");
            }

            return _mapper.Map<CitaDTO>(cita);
        }

        [HttpGet("paciente/{id}")]
        public ActionResult<IEnumerable<CitaDTO>> GetCitasByPaciente(long id)
        {
            IList<CitaDTO> citasDTO = new List<CitaDTO>();
            var citas = _citaService.GetCitasByPaciente(id);

            if (citas == null)
            {
                return BadRequest();
            }
            foreach (Cita c in citas)
            {
                citasDTO.Add(_mapper.Map<CitaDTO>(c));
            }
            return citasDTO.ToList();
        }

        [HttpGet("medico/{id}")]
        public ActionResult<IEnumerable<CitaDTO>> GetCitasByMedico(long id)
        {
            IList<CitaDTO> citasDTO = new List<CitaDTO>();
            var citas = _citaService.GetCitasByMedico(id);

            if(citas == null)
            {
                return BadRequest();
            }
            foreach (Cita c in citas)
            {
                citasDTO.Add(_mapper.Map<CitaDTO>(c));
            }
            return citasDTO.ToList();
        }

        // PUT: api/Citas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutCita(long id, Cita cita)
        {
            if (id != cita.Id)
            {
                return BadRequest();
            }

            _context.Entry(cita).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Citas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Cita> AddCita(CitaDTO cita)
        {
            if (!_citaService.AddCita(_mapper.Map<Cita>(cita), cita.Medico, cita.Paciente))
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }     
        }

        // DELETE: api/Citas/5
        [HttpDelete("{id}")]
        public ActionResult<Cita> DeleteCita(long id)
        {
            if (_citaService.DeleteCita(id) == null)
            {
                return NotFound("Cita no encontrada");
            }
            else
            {
                return Ok();
            }
        }

        private bool CitaExists(long id)
        {
            return _context.Citas.Any(e => e.Id == id);
        }
    }
}
