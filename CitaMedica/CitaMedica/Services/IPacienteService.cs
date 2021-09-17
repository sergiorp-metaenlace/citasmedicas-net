using CitaMedica.Models;
using CitaMedica.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaMedica.Services
{
    public interface IPacienteService
    {

        public IEnumerable<Paciente> GetAllPacientes();

        public Paciente GetPacienteById(long id);

        public Paciente AddPaciente(Paciente paciente);

        public bool AddMedico(long idPaciente, long idMedico);

        public IEnumerable<Medico> GetMedicos(long idPaciente);

        public IEnumerable<Medico> GetMedicosNot(long idPaciente);

        public Paciente DeletePaciente(long id);

        public Paciente Login(string username, string password);
    }
}
