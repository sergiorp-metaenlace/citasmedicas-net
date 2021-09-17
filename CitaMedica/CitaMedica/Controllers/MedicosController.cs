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
    [Route("medicos")]
    [ApiController]
    public class MedicosController : ControllerBase
    {
        private readonly CMContext _context;
        private readonly IMapper _mapper;
        private readonly IMedicoService _medService;

        public MedicosController(CMContext context, IMapper mapper, IMedicoService medService)
        {
            _context = context;
            _mapper = mapper;
            _medService = medService;
        }

        // GET: api/Medicos
        [HttpGet]
        public ActionResult<IEnumerable<MedicoDTO>> GetMedico()
        {
            IList<MedicoDTO> medicosDTO = new List<MedicoDTO>();
            var medicos = _medService.GetAllMedicos();
            foreach (Medico m in medicos)
            {
                medicosDTO.Add(_mapper.Map<MedicoDTO>(m));
            }
            return medicosDTO.ToList();
        }


        // GET: api/Medicos/5
        [HttpGet("{id}")]
        public ActionResult<MedicoDTO> GetMedico(long id)
        {
            var medico = _medService.GetMedicoById(id);

            if (medico == null)
            {
                return NotFound("No se ha encontrado el médico");
            }

            return _mapper.Map<MedicoDTO>(medico);
        }

        [HttpGet("pacientes/{id}")]
        public ActionResult<IEnumerable<PacienteDTO>> GetPacientesByMedico(long id)
        {
            IList<PacienteDTO> pacientesDTO = new List<PacienteDTO>();
            var pacientes = _medService.GetPacientes(id);
            if (pacientes == null)
            {
                return BadRequest("No se encuentra al paciente");
            }
            else
            {
                foreach (Paciente p in pacientes)
                {
                    pacientesDTO.Add(_mapper.Map<PacienteDTO>(p));
                }
                return pacientesDTO.ToList();
            }
        }

        // PUT: api/Medicos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutMedico(long id, Medico medico)
        {
            if (id != medico.Id)
            {
                return BadRequest();
            }

            _context.Entry(medico).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicoExists(id))
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

        // POST: api/Medicos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Medico> PostMedico(Medico medico)
        {
            if (_medService.AddMedico(medico) == null)
            {
                return BadRequest("El médico ya existe");
            }
            else
            {
                return Ok();
            } 
        }

        // POST: api/Medicos/Login
        [HttpPost("login")]
        public ActionResult<Medico> Login(Medico login)
        {
            Medico med = _medService.Login(login.User, login.Clave);
            if (med == null)
            {
                return BadRequest("El medico no existe");
            }
            else
            {
                return Ok(_mapper.Map<MedicoDTO>(med));
            }

        }

        // DELETE: api/Medicos/5
        [HttpDelete("{id}")]
        public ActionResult<Medico> DeleteMedico(long id)
        {
            if (_medService.DeleteMedico(id) == null)
            {
                return NotFound("El médico no existe");
            }
            else
            {
                return Ok();
            }  
        }

        private bool MedicoExists(long id)
        {
            return _context.Medicos.Any(e => e.Id == id);
        }
    }
}
