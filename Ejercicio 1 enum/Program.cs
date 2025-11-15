using Ejercicio_1_enum.Connection;
using Ejercicio_1_enum.Entity;
using Ejercicio_1_enum.Enum;

class Program
{
    static void Main()
    {
        using var db = new AppDbContext();

        db.Database.EnsureDeleted();//elimino y creo para la prueba
        db.Database.EnsureCreated();

          var pacientes = new List<Paciente>
            {
                new Paciente { Nombre = "Juan",   Apellido = "Pérez",    Genero = Genero.Masculino },
                new Paciente { Nombre = "María",  Apellido = "Gómez",    Genero = Genero.Femenino },
                new Paciente { Nombre = "Carlos", Apellido = "Ramírez",  Genero = Genero.Masculino },
                new Paciente { Nombre = "Laura",  Apellido = "Torres",   Genero = Genero.Femenino },
                new Paciente { Nombre = "Alex",   Apellido = "Lozano",   Genero = Genero.Masculino }
            };

        db.Pacientes.AddRange(pacientes);//Guarda la lista 
        db.SaveChanges();//guarda

        //mostrar por la consola los registros
        foreach (var p in db.Pacientes)
        {
            Console.WriteLine($"{p.Id} - {p.Nombre} - {p.Genero}");
        }
    }
}
