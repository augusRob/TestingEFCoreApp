using Microsoft.EntityFrameworkCore;
using TestingEFCoreApp.Models;

namespace TestingEFCoreApp.Data
{
    public class EFCoreAppContext : DbContext
    {

        // Es necesario crear un constructor vacio para poder crear instancias del contexto en cada test unitario.
        // Asi es posible aislar y modularizar las pruebas realizadas.

        public EFCoreAppContext()
        {
        }

        public EFCoreAppContext(DbContextOptions<EFCoreAppContext> options) : base(options) 
        { 
        }
        
        public DbSet<Carrera> Carrera { get; set; }
        public DbSet<Materia> Materia { get; set; }
    }
}
