using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zoo_Animal_Management_System.Models;
using Zoo_Animal_Management_System.Services;
using Zoo_Animal_Management_System.Services.Repository;
using static Zoo_Animal_Management_System.Enums;

namespace ZooAnimalManagementSystemTEst
{
    public class ZooAnimalManagerControllerTests
    {

        [Fact]
        public async Task FillEnclosures_ShouldReturnBadRequest_WhenNoAnimalsAndEnclosures()
        {
            // Arrange
            var animalRepositoryMock = new Mock<IAnimalRepository>();
            var enclosureRepositoryMock = new Mock<IEnclosureRepository>();
            var loggerMock = new Mock<ILogger<AnimalDistributionService>>();

            animalRepositoryMock.Setup(repo => repo.GetAllAnimalsWifouthEnclosure()).ReturnsAsync(new List<Animal>());
            enclosureRepositoryMock.Setup(repo => repo.GetAllEnclosures()).ReturnsAsync(new List<Enclosure>());

            var service = new AnimalDistributionService(animalRepositoryMock.Object, enclosureRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await service.FillEnclosures();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }



        [Fact]
        public async Task GetNotAssignedAnimalsInfo_ShouldReturnBadRequest_WhenNoFreeAnimals()
        {
            // Arrange
            var animalRepositoryMock = new Mock<IAnimalRepository>();
            var enclosureRepositoryMock = new Mock<IEnclosureRepository>();
            var loggerMock = new Mock<ILogger<AnimalDistributionService>>();

            animalRepositoryMock.Setup(repo => repo.GetAllAnimalsWifouthEnclosure()).ReturnsAsync(new List<Animal>());

            var service = new AnimalDistributionService(animalRepositoryMock.Object, enclosureRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await service.GetNotAssignedAnimalsInfo();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetNotAssignedAnimalsInfo_ShouldReturnOkWithAnimals_WhenFreeAnimalsExist()
        {
            // Arrange
            var animalRepositoryMock = new Mock<IAnimalRepository>();
            var enclosureRepositoryMock = new Mock<IEnclosureRepository>();
            var loggerMock = new Mock<ILogger<AnimalDistributionService>>();

            var animals = new List<Animal> { new Animal { Id = Guid.NewGuid(), Species = "Elephant", Food = AnimalFood.Herbivore } };

            animalRepositoryMock.Setup(repo => repo.GetAllAnimalsWifouthEnclosure()).ReturnsAsync(animals);

            var service = new AnimalDistributionService(animalRepositoryMock.Object, enclosureRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await service.GetNotAssignedAnimalsInfo();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(animals, okResult.Value);
        }


        [Fact]
        public async Task GetNotAssignedAnimalsInfo_ShouldReturnOk_WhenAnimalsExistWithoutEnclosure()
        {
            // Arrange
            var animalRepositoryMock = new Mock<IAnimalRepository>();
            var enclosureRepositoryMock = new Mock<IEnclosureRepository>();
            var loggerMock = new Mock<ILogger<AnimalDistributionService>>();

            var animals = new List<Animal> { new Animal { Id = Guid.NewGuid(), Species = "Lion", Food = AnimalFood.Carnivore } };

            animalRepositoryMock.Setup(repo => repo.GetAllAnimalsWifouthEnclosure()).ReturnsAsync(animals);

            var service = new AnimalDistributionService(animalRepositoryMock.Object, enclosureRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await service.GetNotAssignedAnimalsInfo();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(animals, okResult.Value);
        }

        [Fact]
        public async Task AssignOrChangeAnimalEnclosure_ShouldReturnBadRequest_WhenAnimalNotFound()
        {
            // Arrange
            var animalRepositoryMock = new Mock<IAnimalRepository>();
            var enclosureRepositoryMock = new Mock<IEnclosureRepository>();
            var loggerMock = new Mock<ILogger<AnimalDistributionService>>();

            animalRepositoryMock.Setup(repo => repo.GetAnimalById(It.IsAny<Guid>())).ReturnsAsync((Animal)null);

            var service = new AnimalDistributionService(animalRepositoryMock.Object, enclosureRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await service.AssignOrChangeAnimalEnclosure(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task AssignOrChangeAnimalEnclosure_ShouldReturnBadRequest_WhenEnclosureNotFound()
        {
            // Arrange
            var animalRepositoryMock = new Mock<IAnimalRepository>();
            var enclosureRepositoryMock = new Mock<IEnclosureRepository>();
            var loggerMock = new Mock<ILogger<AnimalDistributionService>>();

            animalRepositoryMock.Setup(repo => repo.GetAnimalById(It.IsAny<Guid>())).ReturnsAsync(new Animal());
            enclosureRepositoryMock.Setup(repo => repo.GetEnclosureById(It.IsAny<Guid>())).ReturnsAsync((Enclosure)null);

            var service = new AnimalDistributionService(animalRepositoryMock.Object, enclosureRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await service.AssignOrChangeAnimalEnclosure(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteAnimal_ShouldReturnBadRequest_WhenAnimalNotFound()
        {
            // Arrange
            var animalRepositoryMock = new Mock<IAnimalRepository>();
            var enclosureRepositoryMock = new Mock<IEnclosureRepository>();
            var loggerMock = new Mock<ILogger<AnimalDistributionService>>();

            animalRepositoryMock.Setup(repo => repo.GetAnimalById(It.IsAny<Guid>())).ReturnsAsync((Animal)null);

            var service = new AnimalDistributionService(animalRepositoryMock.Object, enclosureRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await service.DeleteAnimal(Guid.NewGuid());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task RemoveAnimalFromEnclosure_ShouldReturnBadRequest_WhenAnimalNotFound()
        {
            // Arrange
            var animalRepositoryMock = new Mock<IAnimalRepository>();
            var enclosureRepositoryMock = new Mock<IEnclosureRepository>();
            var loggerMock = new Mock<ILogger<AnimalDistributionService>>();

            animalRepositoryMock.Setup(repo => repo.GetAnimalById(It.IsAny<Guid>())).ReturnsAsync((Animal)null);

            var service = new AnimalDistributionService(animalRepositoryMock.Object, enclosureRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await service.RemoveAnimalFromEnclosure(Guid.NewGuid());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task RemoveAnimalFromEnclosure_ShouldReturnOk_WhenAnimalSuccessfullyRemoved()
        {
            // Arrange
            var animalRepositoryMock = new Mock<IAnimalRepository>();
            var enclosureRepositoryMock = new Mock<IEnclosureRepository>();
            var loggerMock = new Mock<ILogger<AnimalDistributionService>>();

            var animalId = Guid.NewGuid();
            var animal = new Animal { Id = animalId, Species = "Giraffe", Food = AnimalFood.Herbivore, Enclosure = new Enclosure() };

            animalRepositoryMock.Setup(repo => repo.GetAnimalById(It.IsAny<Guid>())).ReturnsAsync(animal);
            animalRepositoryMock.Setup(repo => repo.UpdateAnimals()).ReturnsAsync(true);

            var service = new AnimalDistributionService(animalRepositoryMock.Object, enclosureRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await service.RemoveAnimalFromEnclosure(animalId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AssignOrChangeAnimalEnclosure_ShouldReturnOk_WhenAnimalAndEnclosureExist()
        {
            // Arrange
            var animalRepositoryMock = new Mock<IAnimalRepository>();
            var enclosureRepositoryMock = new Mock<IEnclosureRepository>();
            var loggerMock = new Mock<ILogger<AnimalDistributionService>>();

            var animalId = Guid.NewGuid();
            var enclosureId = Guid.NewGuid();

            var animal = new Animal { Id = animalId, Species = "Panda", Food = AnimalFood.Herbivore };
            var enclosure = new Enclosure { Id = enclosureId, Size = EnclosureSize.Large };

            animalRepositoryMock.Setup(repo => repo.GetAnimalById(animalId)).ReturnsAsync(animal);
            enclosureRepositoryMock.Setup(repo => repo.GetEnclosureById(enclosureId)).ReturnsAsync(enclosure);
            animalRepositoryMock.Setup(repo => repo.UpdateAnimals()).ReturnsAsync(true);

            var service = new AnimalDistributionService(animalRepositoryMock.Object, enclosureRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await service.AssignOrChangeAnimalEnclosure(animalId, enclosureId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Animal uploaded sucessfully", okResult.Value);
        }

        [Fact]
        public async Task DeleteAnimal_ShouldReturnOk_WhenAnimalSuccessfullyDeleted()
        {
            // Arrange
            var animalRepositoryMock = new Mock<IAnimalRepository>();
            var enclosureRepositoryMock = new Mock<IEnclosureRepository>();
            var loggerMock = new Mock<ILogger<AnimalDistributionService>>();

            var animalId = Guid.NewGuid();
            var animal = new Animal { Id = animalId, Species = "Kangaroo", Food = AnimalFood.Herbivore };

            animalRepositoryMock.Setup(repo => repo.GetAnimalById(animalId)).ReturnsAsync(animal);
            animalRepositoryMock.Setup(repo => repo.DeleteAnimal(animal)).ReturnsAsync(true);

            var service = new AnimalDistributionService(animalRepositoryMock.Object, enclosureRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await service.DeleteAnimal(animalId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Animal was deleted sucessfully", okResult.Value);
        }

        [Fact]
        public async Task RemoveAnimalFromEnclosure_ShouldReturnBadRequest_WhenUpdateFails()
        {
            // Arrange
            var animalRepositoryMock = new Mock<IAnimalRepository>();
            var enclosureRepositoryMock = new Mock<IEnclosureRepository>();
            var loggerMock = new Mock<ILogger<AnimalDistributionService>>();

            var animalId = Guid.NewGuid();
            var animal = new Animal { Id = animalId, Species = "Zebra", Food = AnimalFood.Herbivore, Enclosure = new Enclosure() };

            animalRepositoryMock.Setup(repo => repo.GetAnimalById(animalId)).ReturnsAsync(animal);
            animalRepositoryMock.Setup(repo => repo.UpdateAnimals()).ReturnsAsync(false);

            var service = new AnimalDistributionService(animalRepositoryMock.Object, enclosureRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await service.RemoveAnimalFromEnclosure(animalId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

  


    }
}
