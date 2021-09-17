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
    [Route("usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly CMContext _context;
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usService;

        public UsuariosController(CMContext context, IMapper mapper, IUsuarioService usService)
        {
            _context = context;
            _mapper = mapper;
            _usService = usService;
        }

        // GET: api/Usuarios
        [HttpGet]
        public ActionResult<IEnumerable<UsuarioDTO>> GetAllUsuarios()
        {
            IList<UsuarioDTO> usuariosDTO = new List<UsuarioDTO>();
            var usuarios = _usService.GetAllUsuarios();
            foreach (Usuario u in usuarios)
            {
                usuariosDTO.Add(_mapper.Map<UsuarioDTO>(u));
            }
            return usuariosDTO.ToList();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public ActionResult<UsuarioDTO> GetUsuarioById(long id)
        {
            var usuario = _usService.GetUsuarioById(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return _mapper.Map<UsuarioDTO>(usuario);
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutUsuario(long id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Usuario> AddUsuario(Usuario usuario)
        {
            if (_usService.AddUsuario(usuario) == null)
            {
                return BadRequest("El usuario ya existe");
            }
            else
            {
                return Ok();
            }  
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public ActionResult<Usuario> DeleteUsuario(long id)
        {
            if (_usService.DeleteUsuario(id) != null)
            {
                return Ok();
            }
            else
            {
                return NotFound("Usuario no encontrado");
            }       
        }

        private bool UsuarioExists(long id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }


    }
}
