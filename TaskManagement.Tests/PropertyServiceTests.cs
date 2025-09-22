using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TaskManagement.Application.Models;
using TaskManagement.Application.Services;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Tests
{
    [TestFixture]
    public class PropertyServiceTests
    {
        private Mock<IPropertyRepository> _mockRepository;
        private PropertyService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IPropertyRepository>();
            _service = new PropertyService(_mockRepository.Object);
        }

        #region SearchAsync Tests

        [Test]
        public async Task SearchAsync_ReturnsMappedDtos_WithImage()
        {
            // Arrange
            var properties = new List<Property>
            {
                new Property { IdProperty = "1", IdOwner = "O1", Name = "Casa Centro", Address = "Av. 1", Price = 100000, Featured = true, CreatedAt = DateTime.UtcNow },
                new Property { IdProperty = "2", IdOwner = "O2", Name = "Depto Norte", Address = "Calle 2", Price = 200000, Featured = false, CreatedAt = DateTime.UtcNow }
            };

            _mockRepository.Setup(r => r.SearchPropertiesAsync("casa", null, null, null, null))
                .ReturnsAsync(properties);
            _mockRepository.Setup(r => r.GetMainImageForPropertyAsync("1"))
                .ReturnsAsync(new PropertyImage { File = "https://img/1.jpg", Enabled = true, IdProperty = "1" });
            _mockRepository.Setup(r => r.GetMainImageForPropertyAsync("2"))
                .ReturnsAsync((PropertyImage?)null);
            _mockRepository.Setup(r => r.HasTransactionsAsync("1"))
                .ReturnsAsync(true);
            _mockRepository.Setup(r => r.HasTransactionsAsync("2"))
                .ReturnsAsync(false);

            // Act
            var result = await _service.SearchAsync("casa", null, null, null, null);

            // Assert
            Assert.That(result, Is.TypeOf<List<PropertyListItemDto>>());
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].ImageUrl, Is.EqualTo("https://img/1.jpg"));
            Assert.That(result[0].HasTransactions, Is.True);
            Assert.That(result[0].Featured, Is.True);
            Assert.That(result[1].ImageUrl, Is.Null);
            Assert.That(result[1].HasTransactions, Is.False);
            Assert.That(result[1].Featured, Is.False);
        }

        [Test]
        public async Task SearchAsync_WithAllFilters_ReturnsFilteredResults()
        {
            // Arrange
            var properties = new List<Property>
            {
                new Property { IdProperty = "1", IdOwner = "O1", Name = "Casa Centro", Address = "Av. 1", Price = 150000, Featured = true, CreatedAt = DateTime.UtcNow }
            };

            _mockRepository.Setup(r => r.SearchPropertiesAsync("casa", "centro", 100000, 200000, "O1"))
                .ReturnsAsync(properties);
            _mockRepository.Setup(r => r.GetMainImageForPropertyAsync("1"))
                .ReturnsAsync((PropertyImage?)null);
            _mockRepository.Setup(r => r.HasTransactionsAsync("1"))
                .ReturnsAsync(false);

            // Act
            var result = await _service.SearchAsync("casa", "centro", 100000, 200000, "O1");

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Casa Centro"));
        }

        #endregion

        #region GetByIdAsync Tests

        [Test]
        public async Task GetByIdAsync_PropertyExists_ReturnsPropertyDetail()
        {
            // Arrange
            var property = new Property
            {
                IdProperty = "1",
                IdOwner = "O1",
                Name = "Casa Centro",
                Address = "Av. 1",
                Price = 100000,
                CodeInternal = "C001",
                Year = 2024,
                Featured = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _mockRepository.Setup(r => r.GetPropertyByIdAsync("1"))
                .ReturnsAsync(property);
            _mockRepository.Setup(r => r.GetMainImageForPropertyAsync("1"))
                .ReturnsAsync(new PropertyImage { File = "https://img/1.jpg", Enabled = true, IdProperty = "1" });
            _mockRepository.Setup(r => r.HasTransactionsAsync("1"))
                .ReturnsAsync(true);

            // Act
            var result = await _service.GetByIdAsync("1");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.IdProperty, Is.EqualTo("1"));
            Assert.That(result.Name, Is.EqualTo("Casa Centro"));
            Assert.That(result.ImageUrl, Is.EqualTo("https://img/1.jpg"));
            // Assert.That(result.HasTransactions, Is.True); // Property not available in DTO
            Assert.That(result.Featured, Is.True);
        }

        [Test]
        public async Task GetByIdAsync_PropertyNotExists_ReturnsNull()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetPropertyByIdAsync("999"))
                .ReturnsAsync((Property?)null);

            // Act
            var result = await _service.GetByIdAsync("999");

            // Assert
            Assert.That(result, Is.Null);
        }

        #endregion

        #region CreateAsync Tests

        [Test]
        public async Task CreateAsync_ValidRequest_ReturnsPropertyId()
        {
            // Arrange
            var request = new CreatePropertyRequest
            {
                Name = "Nueva Casa",
                Address = "Calle Nueva",
                Price = 300000,
                CodeInternal = "C003",
                Year = 2024,
                IdOwner = "O1",
                ImageUrl = "https://img/nueva.jpg",
                ImageEnabled = true,
                Featured = true
            };

            _mockRepository.Setup(r => r.CreatePropertyAsync(It.IsAny<Property>()))
                .ReturnsAsync("new-property-id");

            // Act
            var result = await _service.CreateAsync(request);

            // Assert
            Assert.That(result, Is.EqualTo("new-property-id"));
            _mockRepository.Verify(r => r.CreatePropertyAsync(It.Is<Property>(p =>
                p.Name == "Nueva Casa" &&
                p.Price == 300000 &&
                p.Featured == true)), Times.Once);
        }

        #endregion

        #region UpdateAsync Tests

        [Test]
        public async Task UpdateAsync_PropertyExists_UpdatesSuccessfully()
        {
            // Arrange
            var existingProperty = new Property
            {
                IdProperty = "1",
                IdOwner = "O1",
                Name = "Casa Original",
                Address = "Av. Original",
                Price = 100000,
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            };

            var request = new CreatePropertyRequest
            {
                Name = "Casa Actualizada",
                Address = "Av. Actualizada",
                Price = 150000,
                CodeInternal = "C001",
                Year = 2024,
                IdOwner = "O1",
                ImageUrl = "https://img/actualizada.jpg",
                ImageEnabled = true,
                Featured = true
            };

            _mockRepository.Setup(r => r.GetPropertyByIdAsync("1"))
                .ReturnsAsync(existingProperty);
            _mockRepository.Setup(r => r.UpdatePropertyAsync(It.IsAny<Property>()))
                .Returns(Task.CompletedTask);
            _mockRepository.Setup(r => r.UpdatePropertyImageAsync(It.IsAny<PropertyImage>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.UpdateAsync("1", request);

            // Assert
            Assert.That(result, Is.EqualTo("1"));
            _mockRepository.Verify(r => r.UpdatePropertyAsync(It.Is<Property>(p =>
                p.Name == "Casa Actualizada" &&
                p.Price == 150000 &&
                p.Featured == true)), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_PropertyNotExists_ThrowsException()
        {
            // Arrange
            var request = new CreatePropertyRequest
            {
                Name = "Casa Actualizada",
                Address = "Av. Actualizada",
                Price = 150000,
                IdOwner = "O1"
            };

            _mockRepository.Setup(r => r.GetPropertyByIdAsync("999"))
                .ReturnsAsync((Property?)null);

            // Act & Assert
            try
            {
                await _service.UpdateAsync("999", request);
                Assert.Fail("Expected exception was not thrown");
            }
            catch (ArgumentException ex)
            {
                Assert.That(ex.Message, Does.Contain("Property with ID '999' not found"));
            }
        }

        #endregion

        #region DeleteAsync Tests

        [Test]
        public async Task DeleteAsync_PropertyExists_DeletesSuccessfully()
        {
            // Arrange
            var existingProperty = new Property
            {
                IdProperty = "1",
                Name = "Casa a Eliminar",
                CreatedAt = DateTime.UtcNow
            };

            _mockRepository.Setup(r => r.GetPropertyByIdAsync("1"))
                .ReturnsAsync(existingProperty);
            _mockRepository.Setup(r => r.DeletePropertyAsync("1"))
                .Returns(Task.CompletedTask);

            // Act
            await _service.DeleteAsync("1");

            // Assert
            _mockRepository.Verify(r => r.DeletePropertyAsync("1"), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_PropertyNotExists_ThrowsException()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetPropertyByIdAsync("999"))
                .ReturnsAsync((Property?)null);

            // Act & Assert
            try
            {
                await _service.DeleteAsync("999");
                Assert.Fail("Expected exception was not thrown");
            }
            catch (ArgumentException ex)
            {
                Assert.That(ex.Message, Does.Contain("Property with ID '999' not found"));
            }
        }

        #endregion
    }
}


