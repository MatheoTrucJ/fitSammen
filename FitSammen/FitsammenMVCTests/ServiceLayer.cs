using FitSammen_API.Model;
using FitSammenWebClient.ServiceLayer;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitsammenMVCTests
{
    public class ServiceLayer
    {
        private readonly ClassService _service;

        public ServiceLayer()
        {
            // Arrange

            // Her faker vi IConfiguration til ClassService
            var settings = new Dictionary<string, string?>
            {
                { "ServiceUrlToUse", "https://localhost:7033/api/" }
                // RET URL/port så den matcher din TestAPI
            };

            IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();

            _service = new ClassService(config);
        }

        [Fact]
        public async void GetAllClassesFromApiReturnsList_ReturnListOfClases()
        {
            //Act
            IEnumerable<Class> result = await _service.GetClasses();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetClassByIdFromApi_ReturnsClassWithThatId()
        {
            // Arrange
            int id = 1;

            // Act
            var result = await _service.GetClasses(id); 
            var list = result?.ToList();

            // Assert
            Assert.Single(list);       
            Assert.Equal(id, list[0].Id);
            Assert.Equal(1, list.Count());
        }
    }
}
