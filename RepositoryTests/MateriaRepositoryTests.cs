using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Runtime.InteropServices;
using TestingEFCoreApp.Controllers;
using TestingEFCoreApp.Models;
using TestingEFCoreApp.Repositories;

namespace RepositoryTests
{
    public class MateriaRepositoryTests
    {
        private readonly Mock<IMateriaRepository> _materiaRepositoryMock;

        public MateriaRepositoryTests()
        {
            _materiaRepositoryMock = new Mock<IMateriaRepository>();
        }

        [Fact]
        public void GetById_ShouldReturnMateriaById()
        {
            // Arrange
            var expectedMateria = new Materia() { Id = 1, Nombre = "Arquitectura", Duracion = 1, CarreraId = 1 };

            _materiaRepositoryMock.Setup(repo => repo.GetById(1))
                .Returns(expectedMateria);

            var expectedMateriaDTO = expectedMateria.ToDTO();

            var materiaController = new MateriasControllerWithRepository(_materiaRepositoryMock.Object);

            // Act
            var resultado = materiaController.GetMateriaById(1);

            // Assert
            Assert.IsType<ActionResult<MateriaDTO>>(resultado);

            _materiaRepositoryMock.Verify(repo => repo.GetById(1));

            if (resultado.Result is OkObjectResult okObjectResult)
            {
                var resultadoMateria = okObjectResult.Value as MateriaDTO;

                // Assert
                Assert.NotNull(resultadoMateria);
                Assert.Equal(expectedMateriaDTO.Nombre, resultadoMateria.Nombre);
                Assert.Equal(expectedMateriaDTO.Duracion, resultadoMateria.Duracion);
                // Añade más aserciones según las propiedades que desees comparar
            } else
            {
                // Si el resultado no es OkObjectResult, puedo manejar el error acá
                throw new InvalidOperationException("El resultado no es OkObjectResult");
            }

        }
    }
}
