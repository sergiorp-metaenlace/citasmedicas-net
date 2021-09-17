using CitaMedica.Models;
using CitaMedica.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaMedica.Services
{
    public interface ICitaService
    {

        public IEnumerable<Cita> GetAllCitas();

        public Cita GetCitaById(long id);

        public IEnumerable<Cita> GetCitasByPaciente(long pacienteID);

        public IEnumerable<Cita> GetCitasByMedico(long medicoID);

        public bool AddCita(Cita cita, long idMedico, long idPaciente);

        public Cita DeleteCita(long id);
    }
}
