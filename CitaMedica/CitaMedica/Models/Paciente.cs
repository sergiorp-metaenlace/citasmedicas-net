using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitaMedica.Models
{
    [Table("Pacientes")]
    public class Paciente : Usuario 
    {
        public string NSS { get; set; }
        public string NumTarjeta { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public ICollection<Medico> Medicos { get; set; } = new List<Medico>();

    }
}
