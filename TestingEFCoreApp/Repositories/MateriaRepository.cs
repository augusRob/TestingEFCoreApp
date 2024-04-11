using TestingEFCoreApp.Data;
using TestingEFCoreApp.Models;

namespace TestingEFCoreApp.Repositories
{
    public class MateriaRepository : BaseRepository<Materia>, IMateriaRepository
    {
        public MateriaRepository(EFCoreAppContext appContext) : base(appContext)
        {
        }
    }
}
