using CitaMedica.Models;
using CitaMedica.Models.DTO;
using CitaMedica.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaMedica.Services
{
    public class DiagnosticoService : IDiagnosticoService
    {
        private CMContext _context;

        public DiagnosticoService(CMContext context)
        {
            _context = context;
        }
        public Diagnostico AddDiagnostico(Diagnostico diagnostico)
        {
            _context.Diagnosticos.Add(diagnostico);
            _context.SaveChanges();

            return diagnostico;
        }

        public Diagnostico DeleteDiagnostico(long id)
        {
            var diagnostico = _context.Diagnosticos.Find(id);
            if (diagnostico == null)
            {
                return null;
            }

            _context.Diagnosticos.Remove(diagnostico);
            _context.SaveChanges();

            return diagnostico;
        }

        public IEnumerable<Diagnostico> GetAllDiagnosticos()
        {
            return _context.Diagnosticos.ToList();
        }

        public Diagnostico GetDiagnosticoById(long id)
        {
            return _context.Diagnosticos.Find(id);
        }

    }
}
