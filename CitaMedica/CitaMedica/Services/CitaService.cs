using CitaMedica.Models;
using CitaMedica.Models.DTO;
using CitaMedica.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaMedica.Services
{
    public class CitaService : ICitaService
    {
        private CMContext _context;

        public CitaService (CMContext context)
        {
            _context = context;
        }
        public bool AddCita(Cita cita, long idMedico, long idPaciente)
        {
            if (cita == null)
                return false;

            Medico med = _context.Medicos.Find(idMedico);
            Paciente pac = _context.Pacientes.Find(idPaciente);
            if (med == null || pac == null)
                return false;

            cita.Medico = med;
            cita.Paciente = pac;

            _context.Citas.Add(cita);

            _context.SaveChanges();

            return true;
        }

        public Cita DeleteCita(long id)
        {
            var cita = _context.Citas.Find(id);
            if (cita == null)
            {
                return null;
            }

            _context.Citas.Remove(cita);
            _context.SaveChanges();

            return cita;
        }

        public IEnumerable<Cita> GetAllCitas()
        {
            return _context.Citas.Include(c => c.Diagnostico).Include(c => c.Medico).Include(c => c.Paciente).ToList();
        }

        public Cita GetCitaById(long id)
        {
            return _context.Citas.Where(c => c.Id == id).Include(c => c.Diagnostico).Include(c => c.Medico).Include(c => c.Paciente).FirstOrDefault();
        }

        public IEnumerable<Cita> GetCitasByMedico(long medicoID)
        {
            var medico = _context.Medicos.Find(medicoID);

            if (medico == null)
                return null;

            return _context.Citas.Where(c => c.Medico == medico).Include(c => c.Diagnostico).Include(c => c.Medico).Include(c => c.Paciente).ToList();
        }

        public IEnumerable<Cita> GetCitasByPaciente(long pacienteID)
        {
            var paciente = _context.Pacientes.Find(pacienteID);

            if (paciente == null)
                return null;

            return _context.Citas.Where(c => c.Paciente == paciente).Include(c => c.Diagnostico).Include(c => c.Medico).Include(c => c.Paciente).ToList();
        }
    }
}
