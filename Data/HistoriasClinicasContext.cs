using Historias_Clinicas_D.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Historias_Clinicas_D.Data
{
    public class HistoriasClinicasContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public HistoriasClinicasContext(DbContextOptions opcionesDeConfiguraciones) : base(opcionesDeConfiguraciones)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser<int>>().ToTable("Personas");
            builder.Entity<IdentityRole<int>>().ToTable("Roles");
            builder.Entity<IdentityUserRole<int>>().ToTable("PersonasRoles");
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Epicrisis> Epicrisis { get; set; }
        public DbSet<Episodio> Episodios { get; set; }
        public DbSet<Evolucion> Evoluciones { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<Telefono> Telefonos { get; set; }
    }
}
