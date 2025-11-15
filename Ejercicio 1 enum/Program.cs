using Ejercicio_1_enum.Connection;
using Ejercicio_1_enum.Entity;
using Ejercicio_1_enum.Enum;

class Program
{
    static void Main()
    {
        using var db = new AppDbContext();

        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

          var pacientes = new List<Paciente>
            {
                new Paciente { Nombre = "Juan",   Apellido = "Pérez",    Genero = Genero.Masculino },
                new Paciente { Nombre = "María",  Apellido = "Gómez",    Genero = Genero.Femenino },
                new Paciente { Nombre = "Carlos", Apellido = "Ramírez",  Genero = Genero.Masculino },
                new Paciente { Nombre = "Laura",  Apellido = "Torres",   Genero = Genero.Femenino },
                new Paciente { Nombre = "Alex",   Apellido = "Lozano",   Genero = Genero.Masculino }
            };

        db.Pacientes.AddRange(pacientes);
        db.SaveChanges();

        foreach (var p in db.Pacientes)
        {
            Console.WriteLine($"{p.Id} - {p.Nombre} - {p.Genero}");
        }
    }
}
