using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CitaMedica.Models;
using CitaMedica.Repositories;
using CitaMedica.Models.DTO;
using CitaMedica.Services;
using AutoMapper;

namespace CitaMedica.Controllers
{
    [Route("pacientes")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly CMContext _context;
        private readonly IMapper _mapper;
        private readonly IPacienteService _pacService;

        public PacientesController(CMContext context, IMapper mapper, IPacienteService pacService)
        {
            _context = context;
            _mapper = mapper;
            _pacService = pacService;
        }

        // GET: api/Pacientes
        [HttpGet]
        public ActionResult<IEnumerable<PacienteDTO>> GetPacientes()
        {
            IList<PacienteDTO> pacientesDTO = new List<PacienteDTO>();
            var pacientes = _pacService.GetAllPacientes();
            foreach (Paciente p in pacientes)
            {
                pacientesDTO.Add(_mapper.Map<PacienteDTO>(p));
            }
            return pacientesDTO.ToList();
        }

        // GET: api/Pacientes/5
        [HttpGet("{id}")]
        public ActionResult<PacienteDTO> GetPacienteById(long id)
        {
            var paciente = _pacService.GetPacienteById(id);

            if (paciente == null)
            {
                return NotFound("Paciente no encontrado");
            }

            return _mapper.Map<PacienteDTO>(paciente);
        }

        [HttpGet("medicos/{id}")]
        public ActionResult<IEnumerable<MedicoDTO>> GetMedicosByPaciente(long id)
        {
            IList<MedicoDTO> medicosDTO = new List<MedicoDTO>();
            var medicos = _pacService.GetMedicos(id);
            if(medicos == null)
            {
                return BadRequest("No se encuentra al paciente");
            }
            else
            {
                foreach (Medico m in medicos)
                {
                    medicosDTO.Add(_mapper.Map<MedicoDTO>(m));
                }
                return medicosDTO.ToList();
            }
        }

        [HttpGet("notmedicos/{id}")]
        public ActionResult<IEnumerable<MedicoDTO>> GetMedicosNotByPaciente(long id)
        {
            IList<MedicoDTO> medicosDTO = new List<MedicoDTO>();
            var medicos = _pacService.GetMedicosNot(id);
            if (medicos == null)
            {
                return BadRequest("No se encuentra al paciente");
            }
            else
            {
                foreach (Medico m in medicos)
                {
                    medicosDTO.Add(_mapper.Map<MedicoDTO>(m));
                }
                return medicosDTO.ToList();
            }
        }


        // PUT: api/Pacientes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutPaciente(long id, Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return BadRequest();
            }

            _context.Entry(paciente).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(id))
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

        // POST: api/Pacientes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Paciente> PostPaciente(Paciente paciente)
        {
            if (_pacService.AddPaciente(paciente) == null)
            {
                return BadRequest("El paciente ya existe");
            }
            else
            {
                return Ok();
            }
        }
        // POST: api/Pacientes/Login
        [HttpPost("login")]
        public ActionResult<Paciente> Login(LoginDTO login)
        {
            Paciente pac = _pacService.Login(login.User, login.Clave);
            if (pac == null)
            {
                return BadRequest("El paciente no existe");
            }
            else
            {
                return Ok(_mapper.Map<PacienteDTO>(pac));
            }
            
        }

        [HttpPost("medico")]
        public ActionResult AddMedico(MedicoPacienteIdDTO medicoPacienteId)
        {
            if(_pacService.AddMedico(medicoPacienteId.PacienteId, medicoPacienteId.MedicoId))
            {
                return Ok();
            }
            else
            {
                return BadRequest("El médico o el paciente no existen, o el médico ya pertenece al paciente");
            }
        }

        // DELETE: api/Pacientes/5
        [HttpDelete("{id}")]
        public ActionResult DeletePaciente(long id)
        {
            if (_pacService.DeletePaciente(id) == null)
            {
                return NotFound("Paciente no encontrado");
            }
            else
            {
                return Ok();
            } 
        }

        private bool PacienteExists(long id)
        {
            return _context.Pacientes.Any(e => e.Id == id);
        }
    }
}
