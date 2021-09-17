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
    public class MedicoService : IMedicoService
    {
        private CMContext _context;

        public MedicoService(CMContext context)
        {
            _context = context;
        }
        public Medico AddMedico(Medico medico)
        {
            if (_context.Medicos.Any(e => e.User == medico.User))
                return null;

            _context.Medicos.Add(medico);
            _context.SaveChanges();

            return medico;
        }

        public Medico Login(string username, string password)
        {
            if (_context.Medicos.Any(e => e.User == username && e.Clave == password))
            {
                return _context.Medicos.Where(e => e.User == username).FirstOrDefault();
            }

            return null;
        }

        public Medico DeleteMedico(long id)
        {
            var medico = _context.Medicos.Find(id);
            if (medico == null)
            {
                return null;
            }

            _context.Medicos.Remove(medico);
            _context.SaveChanges();

            return medico;
        }

        public IEnumerable<Medico> GetAllMedicos()
        {
            return _context.Medicos.ToList();
        }

        public IEnumerable<Paciente> GetPacientes(long idMedico)
        {
            Medico mec = _context.Medicos.Where(m => m.Id == idMedico).Include(m => m.Pacientes).FirstOrDefault();

            if (mec == null)
                return null;

            return mec.Pacientes;
        }

        public Medico GetMedicoById(long id)
        {
            return _context.Medicos.Find(id);
        }

    }
}
