using CitaMedica.Models;
using CitaMedica.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaMedica.Services
{
    public interface IDiagnosticoService
    {
        public IEnumerable<Diagnostico> GetAllDiagnosticos();

        public Diagnostico GetDiagnosticoById(long id);

        public Diagnostico AddDiagnostico(Diagnostico diagnostico);

        public Diagnostico DeleteDiagnostico(long id);
    }
}
