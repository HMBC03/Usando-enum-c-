using Ejercicio_1_enum.Entity;
using Microsoft.EntityFrameworkCore;

namespace Ejercicio_1_enum.Connection
{
    public class AppDbContext : DbContext
    {
        public DbSet<Paciente> Pacientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Si usas enum → int (por defecto)
            modelBuilder.Entity<Paciente>()
                .Property(p => p.Genero);
            //     .HasConversion<int>();

            // Si quieres guardar enum como string
            // modelBuilder.Entity<Paciente>()
            //     .Property(p => p.Genero)
            //     .HasConversion<string>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Cambia los valores según tu entorno
            var connectionString =
                "server=localhost;port=3306;database=ejemplo1;user=root;password=root;";

            optionsBuilder.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            );
        }
    }
}
