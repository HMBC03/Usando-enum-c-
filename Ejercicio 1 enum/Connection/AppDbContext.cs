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

            //aqui gardamos el enum y EF lo guarda como int por defecto
            modelBuilder.Entity<Paciente>()
                .Property(p => p.Genero);


            // Para el segundo ejemplo se puede guardar como string
            // modelBuilder.Entity<Paciente>()
            //     .Property(p => p.Genero)
            //     .HasConversion<string>(); aca le dice que lo quiere guardar como string
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Cambia los valores según tu entorno
            var connectionString =
                "server=localhost;port=3306;database=ejemplo1;user=root;password=root;"; //la conexion de toda la vida

            optionsBuilder.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            );
        }
    }
}
