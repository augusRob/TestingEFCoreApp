using TestingEFCoreApp.Data;
using TestingEFCoreApp.Models;

namespace TestingEFCoreApp.Repositories
{
    public class CarreraRepository : BaseRepository<Carrera>, ICarreraRepository
    {
        public CarreraRepository(EFCoreAppContext appContext) : base(appContext)
        {
        }
    }
}
