using CitaMedica.Models;
using CitaMedica.Models.DTO;
using CitaMedica.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaMedica.Services
{
    public class UsuarioService : IUsuarioService
    {
        private CMContext _context;

        public UsuarioService(CMContext context)
        {
            _context = context;
        }
        public Usuario AddUsuario(Usuario usuario)
        {
            if (_context.Usuarios.Any(e => e.User == usuario.User))
                return null;

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return usuario;
        }

        public Usuario DeleteUsuario(long id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
            {
                return null;
            }

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            return usuario;
        }

        public IEnumerable<Usuario> GetAllUsuarios()
        {
            return _context.Usuarios.ToList(); 
        }

        public Usuario GetUsuarioById(long id)
        {
            return _context.Usuarios.Find(id);
        }
    }
}
