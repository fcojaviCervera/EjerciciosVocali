using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Ejercicio3
{
    public class SustitucionesContext : DbContext
    {
        public DbSet<Sustituciones> Sustituciones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["LocalDB"].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SustitucionesTexto>();
            modelBuilder.Entity<SustitucionesTeclado>();
            modelBuilder.Entity<SustitucionesCompuestas>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
