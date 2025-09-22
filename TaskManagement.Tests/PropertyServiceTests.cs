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
    public class PropertyServiceTests
    {
        [Test]
        public async Task SearchAsync_ReturnsMappedDtos_WithImage()
        {
            var properties = new List<Property>
            {
                new Property { IdProperty = "1", IdOwner = "O1", Name = "Casa Centro", Address = "Av. 1", Price = 100000 },
                new Property { IdProperty = "2", IdOwner = "O2", Name = "Depto Norte", Address = "Calle 2", Price = 200000 }
            };

            var repo = new Mock<IPropertyRepository>();
            repo.Setup(r => r.SearchPropertiesAsync("casa", null, null, null, null))
                .ReturnsAsync(properties);
            repo.Setup(r => r.GetMainImageForPropertyAsync("1"))
                .ReturnsAsync(new PropertyImage { File = "https://img/1.jpg", Enabled = true, IdProperty = "1" });
            repo.Setup(r => r.GetMainImageForPropertyAsync("2"))
                .ReturnsAsync((PropertyImage?)null);

            var service = new PropertyService(repo.Object);

            var result = await service.SearchAsync("casa", null, null, null, null);

            Assert.That(result, Is.TypeOf<List<PropertyListItemDto>>());
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].ImageUrl, Is.EqualTo("https://img/1.jpg"));
            Assert.That(result[1].ImageUrl, Is.Null);
        }
    }
}


