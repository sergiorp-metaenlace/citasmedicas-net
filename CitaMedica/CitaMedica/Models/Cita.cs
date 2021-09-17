using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CitaMedica.Models
{
    public class Cita
    {
        [Key]
        public long Id { get; set; }
        public string MotivoCita { get; set; }
        public DateTime FechaHora { get; set; }
        public int Attribute11 { get; set; }
        public Diagnostico Diagnostico { get; set; }
        public Medico Medico { get; set; }
        public Paciente Paciente { get; set; }
    }
}
