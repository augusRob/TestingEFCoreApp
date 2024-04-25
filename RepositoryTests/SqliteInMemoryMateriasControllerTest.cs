using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using TestingEFCoreApp.Controllers;
using TestingEFCoreApp.Data;
using TestingEFCoreApp.Models;

// Se crea una conexión a una base de datos SQLite in-memory.
// Se configuran las opciones del contexto para dicha base de datos.
// Dicha configuración es pasada al appContext de nuestra aplicación.
// Se popula la base de datos en memoria con los registros que sean necesarios para nuestro set de pruebas.
// Se crea un metodo para la instancia y creacion de contexto de base de datos SQLite individuales y aislados para cada test unitario.
// En cada test unitario se utilizará dicho contexto inyectado en los controladores correspondientes para ser testeado.

namespace TestingWithoutDatabase
{
    public class SqliteInMemoryMateriasControllerTest : IDisposable
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<EFCoreAppContext> _contextOptions;

        public SqliteInMemoryMateriasControllerTest()
        {
            // Create and open a connection. This creates the SQLite in-memory database, which will persist until
            // the connection is closed at the end of the test (see Disposable below).
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            // These options will be used by the context instances in this test suite, including the connection opened above.
            _contextOptions = new DbContextOptionsBuilder<EFCoreAppContext>()
                .UseSqlite(_connection)
                .Options;

            // Create the schema and seed some data
            using var context = new EFCoreAppContext(_contextOptions);
            context.Database.EnsureCreated();

            // Seed the database
            context.AddRange(
                new Carrera { Id = 1, Nombre = "Arquitectura", Titulo = "Arquitecto", Duracion = 10 },
                new Materia { Id = 1, Nombre = "Arquitectura", Duracion = 1, CarreraId = 1 },
                new Materia { Id = 2, Nombre = "Urbanismo", Duracion = 1, CarreraId = 1 }
            );

            context.SaveChanges();
        }

        EFCoreAppContext CreateContext() => new EFCoreAppContext(_contextOptions);
        public void Dispose() => _connection.Dispose();

        [Fact]
        public void GetMateriaByName_ShouldReturnSpecificMateria()
        {
            using var context = CreateContext();
            var materiasController = new MateriasController( context );

            var materia = materiasController.GetMateriaByName("Urbanismo").Value;

            Assert.Equal(1, materia.CarreraId);
        }

        [Fact]
        public void GetMateriaById_ShouldReturnMateria()
        {
            using var context = CreateContext();
            var materiasController = new MateriasController(context);

            var materia = materiasController.GetMateriaById(2).Value;

            Assert.Equal("Urbanismo", materia.Nombre);
        }
    }
}
