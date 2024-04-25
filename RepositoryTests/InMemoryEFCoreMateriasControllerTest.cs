using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TestingEFCoreApp.Controllers;
using TestingEFCoreApp.Data;
using TestingEFCoreApp.Models;

// Se configuran las opciones del contexto para la base de datos en memoria provista por EF Core.
// Dicha configuración es pasada al appContext de nuestra aplicación.
// Se popula la base de datos en memoria con los registros que sean necesarios para nuestro set de pruebas.
// Se crea un metodo para la instancia y creacion de contexto de base de datos en memoria individuales y aislados para cada test unitario.
// En cada test unitario se utilizará dicho contexto inyectado en los controladores correspondientes para ser testeado.

namespace TestingWithoutDatabase
{
    public class InMemoryEFCoreMateriasControllerTest
    {
        private readonly DbContextOptions<EFCoreAppContext> _contextOptions;
        public InMemoryEFCoreMateriasControllerTest()
        {
            _contextOptions = new DbContextOptionsBuilder<EFCoreAppContext>()
                .UseInMemoryDatabase(databaseName: "MateriasControllerTest")
                .ConfigureWarnings(m => m.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            using var context = new EFCoreAppContext(_contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.AddRange(
                new Carrera { Id = 1, Nombre = "Arquitectura", Titulo = "Arquitecto", Duracion = 10 },
                new Materia { Id = 1, Nombre = "Arquitectura", Duracion = 1, CarreraId = 1 },
                new Materia { Id = 2, Nombre = "Urbanismo", Duracion = 1, CarreraId = 1 }
                );

            context.SaveChanges();
             
        }

        EFCoreAppContext CreateContext() => new EFCoreAppContext(_contextOptions);

        [Fact]
        public void GetMateriaByName_ShouldReturnSpecificMateria()
        {
            using var context = CreateContext();
            var materiasController = new MateriasController(context);

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
