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
    public class PropertyTraceServiceTests
    {
        private Mock<IPropertyTraceRepository> _mockRepository;
        private PropertyTraceService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IPropertyTraceRepository>();
            _service = new PropertyTraceService(_mockRepository.Object);
        }

        #region CreateAsync Tests

        [Test]
        public async Task CreateAsync_ValidRequest_ReturnsTraceId()
        {
            // Arrange
            var request = new CreatePropertyTraceRequest
            {
                IdProperty = "1",
                DateSale = DateTime.UtcNow,
                Name = "Comprador Test",
                Value = 500000,
                Tax = 7500
            };

            _mockRepository.Setup(r => r.CreatePropertyTraceAsync(It.IsAny<PropertyTrace>()))
                .ReturnsAsync("new-trace-id");

            // Act
            var result = await _service.CreateAsync(request);

            // Assert
            Assert.That(result, Is.EqualTo("new-trace-id"));
            _mockRepository.Verify(r => r.CreatePropertyTraceAsync(It.Is<PropertyTrace>(t => 
                t.IdProperty == "1" && 
                t.Name == "Comprador Test" && 
                t.Value == 500000)), Times.Once);
        }

        #endregion

        #region GetByPropertyIdAsync Tests

        [Test]
        public async Task GetByPropertyIdAsync_PropertyHasTraces_ReturnsTraceList()
        {
            // Arrange
            var traces = new List<PropertyTrace>
            {
                new PropertyTrace
                {
                    IdPropertyTrace = "1",
                    IdProperty = "1",
                    Name = "Comprador 1",
                    Value = 500000,
                    Tax = 7500,
                    DateSale = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                }
            };

            _mockRepository.Setup(r => r.GetTracesByPropertyIdAsync("1"))
                .ReturnsAsync(traces);

            // Act
            var result = await _service.GetByPropertyIdAsync("1");

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].IdPropertyTrace, Is.EqualTo("1"));
            Assert.That(result[0].Name, Is.EqualTo("Comprador 1"));
            Assert.That(result[0].Value, Is.EqualTo(500000));
        }

        [Test]
        public async Task GetByPropertyIdAsync_PropertyHasNoTraces_ReturnsEmptyList()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetTracesByPropertyIdAsync("1"))
                .ReturnsAsync(new List<PropertyTrace>());

            // Act
            var result = await _service.GetByPropertyIdAsync("1");

            // Assert
            Assert.That(result.Count, Is.EqualTo(0));
        }

        #endregion

        #region GetByIdAsync Tests

        [Test]
        public async Task GetByIdAsync_TraceExists_ReturnsTraceDetail()
        {
            // Arrange
            var trace = new PropertyTrace
            {
                IdPropertyTrace = "1",
                IdProperty = "1",
                Name = "Comprador Test",
                Value = 500000,
                Tax = 7500,
                DateSale = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _mockRepository.Setup(r => r.GetPropertyTraceByIdAsync("1"))
                .ReturnsAsync(trace);

            // Act
            var result = await _service.GetByIdAsync("1");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.IdPropertyTrace, Is.EqualTo("1"));
            Assert.That(result.Name, Is.EqualTo("Comprador Test"));
            Assert.That(result.Value, Is.EqualTo(500000));
        }

        [Test]
        public async Task GetByIdAsync_TraceNotExists_ReturnsNull()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetPropertyTraceByIdAsync("999"))
                .ReturnsAsync((PropertyTrace?)null);

            // Act
            var result = await _service.GetByIdAsync("999");

            // Assert
            Assert.That(result, Is.Null);
        }

        #endregion

        #region UpdateAsync Tests

        [Test]
        public async Task UpdateAsync_TraceExists_UpdatesSuccessfully()
        {
            // Arrange
            var existingTrace = new PropertyTrace
            {
                IdPropertyTrace = "1",
                IdProperty = "1",
                Name = "Comprador Original",
                Value = 500000,
                Tax = 7500,
                DateSale = DateTime.UtcNow.AddDays(-1),
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            };

            var request = new CreatePropertyTraceRequest
            {
                IdProperty = "1",
                Name = "Comprador Actualizado",
                Value = 600000,
                Tax = 9000,
                DateSale = DateTime.UtcNow
            };

            _mockRepository.Setup(r => r.GetPropertyTraceByIdAsync("1"))
                .ReturnsAsync(existingTrace);
            _mockRepository.Setup(r => r.UpdatePropertyTraceAsync(It.IsAny<PropertyTrace>()))
                .Returns(Task.CompletedTask);

            // Act
            await _service.UpdateAsync("1", request);

            // Assert
            _mockRepository.Verify(r => r.UpdatePropertyTraceAsync(It.Is<PropertyTrace>(t => 
                t.Name == "Comprador Actualizado" && 
                t.Value == 600000)), Times.Once);
        }

        #endregion

        #region DeleteAsync Tests

        [Test]
        public async Task DeleteAsync_TraceExists_DeletesSuccessfully()
        {
            // Arrange
            var existingTrace = new PropertyTrace
            {
                IdPropertyTrace = "1",
                IdProperty = "1",
                Name = "Comprador a Eliminar",
                CreatedAt = DateTime.UtcNow
            };

            _mockRepository.Setup(r => r.GetPropertyTraceByIdAsync("1"))
                .ReturnsAsync(existingTrace);
            _mockRepository.Setup(r => r.DeletePropertyTraceAsync("1"))
                .Returns(Task.CompletedTask);

            // Act
            await _service.DeleteAsync("1");

            // Assert
            _mockRepository.Verify(r => r.DeletePropertyTraceAsync("1"), Times.Once);
        }

        #endregion
    }
}
