namespace TestingEFCoreApp.Models
{
    public class Materia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public int CarreraId { get; set; }
        public Carrera Carrera { get; set; }
        
    }

    public static class MateriaExtensions
    {
        public static MateriaDTO ToDTO(this Materia materia)
        {
            return new MateriaDTO()
            {
                Id = materia.Id,
                Nombre = materia.Nombre,
                Duracion = materia.Duracion,
                CarreraId = materia.CarreraId
            };
        }
    }
}
