using CitaMedica.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaMedica.Repositories
{

    public class CMContext : DbContext
    {
        public CMContext(DbContextOptions<CMContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Cita> Citas { get; set; }

        public DbSet<Paciente> Pacientes { get; set; }

        public DbSet<Medico> Medicos { get; set; }

        public DbSet<Diagnostico> Diagnosticos { get; set; }
    }

}
