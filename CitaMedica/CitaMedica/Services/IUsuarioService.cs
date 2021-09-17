using CitaMedica.Models.DTO;
using CitaMedica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaMedica.Services
{
    public interface IUsuarioService
    {

        public IEnumerable<Usuario> GetAllUsuarios();

        public Usuario GetUsuarioById(long id);

        public Usuario AddUsuario(Usuario usuario);

        public Usuario DeleteUsuario(long id);
    }
}
