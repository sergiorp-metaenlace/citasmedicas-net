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
    [Route("diagnosticos")]
    [ApiController]
    public class DiagnosticosController : ControllerBase
    {
        private readonly CMContext _context;
        private readonly IMapper _mapper;
        private readonly IDiagnosticoService _diaService;

        public DiagnosticosController(CMContext context, IMapper mapper, IDiagnosticoService diaService)
        {
            _context = context;
            _mapper = mapper;
            _diaService = diaService;
        }

        // GET: api/Diagnosticos
        [HttpGet]
        public ActionResult<IEnumerable<DiagnosticoDTO>> GetAllDiagnosticos()
        {
            IList<DiagnosticoDTO> diagnosticosDTO = new List<DiagnosticoDTO>();
            var diagnosticos = _diaService.GetAllDiagnosticos();
            foreach (Diagnostico d in diagnosticos)
            {
                diagnosticosDTO.Add(_mapper.Map<DiagnosticoDTO>(d));
            }
            return diagnosticosDTO.ToList();
        }

        // GET: api/Diagnosticos/5
        [HttpGet("{id}")]
        public ActionResult<DiagnosticoDTO> GetDiagnostico(long id)
        {
            var diagnostico = _diaService.GetDiagnosticoById(id);

            if (diagnostico == null)
            {
                return NotFound("Diagnostico no encontrado");
            }

            return _mapper.Map<DiagnosticoDTO>(diagnostico);
        }

        // PUT: api/Diagnosticos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutDiagnostico(long id, Diagnostico diagnostico)
        {
            if (id != diagnostico.Id)
            {
                return BadRequest();
            }

            _context.Entry(diagnostico).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            { 
                if (!DiagnosticoExists(id))
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

        // POST: api/Diagnosticos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Diagnostico> PostDiagnostico(Diagnostico diagnostico)
        {
            if(_diaService.AddDiagnostico(diagnostico) == null)
            {
                return BadRequest("");
            }
            else
            {
                return Ok();
            }
        }

        // DELETE: api/Diagnosticos/5
        [HttpDelete("{id}")]
        public ActionResult<Diagnostico> DeleteDiagnostico(long id)
        {

            if (_diaService.DeleteDiagnostico(id) == null)
            {
                return NotFound("Diagnostico no encontrado");
            }
            else
            {
                return Ok();
            }
        }

        private bool DiagnosticoExists(long id)
        {
            return _context.Diagnosticos.Any(e => e.Id == id);
        }
    }
}
