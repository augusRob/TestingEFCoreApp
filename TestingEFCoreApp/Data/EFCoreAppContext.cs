using Microsoft.EntityFrameworkCore;
using TestingEFCoreApp.Models;

namespace TestingEFCoreApp.Data
{
    public class EFCoreAppContext : DbContext
    {
        public EFCoreAppContext(DbContextOptions<EFCoreAppContext> options) : base(options) 
        { 
        }
        
        public DbSet<Carrera> Carrera { get; set; }
        public DbSet<Materia> Materia { get; set; }
    }
}
