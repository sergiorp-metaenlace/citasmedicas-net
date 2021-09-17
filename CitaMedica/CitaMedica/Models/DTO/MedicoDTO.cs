using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaMedica.Models.DTO
{
    public class MedicoDTO
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string User { get; set; }
        public string Clave { get; set; }
        public string NumColegiado { get; set; }
        public ICollection<long> Pacientes { get; set; }
    }
}
