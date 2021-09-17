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
    public class PacienteService : IPacienteService
    {
        private CMContext _context;

        public PacienteService(CMContext context)
        {
            _context = context;
        }
        public Paciente AddPaciente(Paciente paciente)
        {
            if (_context.Pacientes.Any(e => e.User == paciente.User))
                return null;

            _context.Pacientes.Add(paciente);
            _context.SaveChanges();

            return paciente;
        }

        public Paciente Login(string username, string password)
        {
            if(_context.Pacientes.Any(e => e.User == username && e.Clave == password))
            {
                return _context.Pacientes.Where(e => e.User == username).FirstOrDefault();
            }

            return null;
        }

        public Paciente DeletePaciente(long id)
        {
            var paciente = _context.Pacientes.Find(id);
            if (paciente == null)
            {
                return null;
            }

            _context.Pacientes.Remove(paciente);
            _context.SaveChanges();

            return paciente;
        }

        public Paciente GetPacienteById(long id)
        {
            return _context.Pacientes.Where(p => p.Id == id).Include(p => p.Medicos).FirstOrDefault();
        }

        public IEnumerable<Paciente> GetAllPacientes()
        {
            return _context.Pacientes.Include(p => p.Medicos).ToList();
        }

        public bool AddMedico(long idPaciente, long idMedico)
        {
            Paciente pac = _context.Pacientes.Find(idPaciente);
            Medico med = _context.Medicos.Find(idMedico);

            if (pac == null || med == null)
                return false;

            if (pac.Medicos.Contains(med))
                return false;

            if (!med.Pacientes.Contains(pac))
                med.Pacientes.Add(pac);

            pac.Medicos.Add(med);
            _context.SaveChanges();

            return true;
        }

        public IEnumerable<Medico> GetMedicos(long idPaciente)
        {
            Paciente pac = _context.Pacientes.Where(p => p.Id == idPaciente).Include(p => p.Medicos).FirstOrDefault();

            if (pac == null)
                return null;

            return pac.Medicos;
        }
        public IEnumerable<Medico> GetMedicosNot(long idPaciente)
        {
            Paciente pac = _context.Pacientes.Where(p => p.Id == idPaciente).Include(p => p.Medicos).FirstOrDefault();
            ICollection<Medico> allMedicos = _context.Medicos.ToList();

            if (pac == null)
                return null;

            return allMedicos.Except(pac.Medicos);
        }

    }
}
