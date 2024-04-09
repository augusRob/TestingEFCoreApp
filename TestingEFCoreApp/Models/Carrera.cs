namespace TestingEFCoreApp.Models
{
    public class Carrera
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public int Duracion { get; set; }
        public ICollection<Materia> Materias { get; set; }

    }
}
