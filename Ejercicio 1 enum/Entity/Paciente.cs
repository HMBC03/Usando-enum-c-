using Ejercicio_1_enum.Enum;

namespace Ejercicio_1_enum.Entity
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Genero Genero { get; set; }
    }
}
