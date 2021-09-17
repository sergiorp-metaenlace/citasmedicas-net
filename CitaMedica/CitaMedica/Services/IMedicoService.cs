using CitaMedica.Models;
using CitaMedica.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaMedica.Services
{
    public interface IMedicoService
    {
        public IEnumerable<Medico> GetAllMedicos();

        public Medico GetMedicoById(long id);

        public IEnumerable<Paciente> GetPacientes(long idMedico);

        public Medico AddMedico(Medico medico);

        public Medico Login(string username, string password);

        public Medico DeleteMedico(long id);
    }
}
