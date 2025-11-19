using FitSammenWebClient.Models;
using FitSammenWebClient.ServiceLayer;
using Microsoft.Extensions.Configuration;

namespace FitsammenMVCTests
{
    public class ServiceLayerTest
    {
        private readonly ClassService _service;

        public ServiceLayerTest()
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
        public async Task GetAllClassesFromApiReturnsList_ReturnListOfClases()
        {
            //Act
            var result = (IEnumerable<Class>?)await _service.GetClasses();

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
            Assert.Single(list); // // hvis endpointet returnerer præcis én class
            Assert.Equal(id, list[0].ClassId);
            Assert.Equal(1, list.Count());
        }
    }
}
