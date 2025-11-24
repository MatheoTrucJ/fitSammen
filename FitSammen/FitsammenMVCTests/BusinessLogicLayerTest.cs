using FitSammenWebClient.BusinessLogicLayer;
using FitSammenWebClient.Models;
using Microsoft.Extensions.Configuration;
using System.Transactions;
namespace FitsammenMVCTests
{
    public class BusinessLogicLayerTest
    {
        private static ClassLogic CreateClassLogic()
        {
            var settings = new Dictionary<string, string?>
            {
                { "ServiceUrlToUse", "https://localhost:7229/api/" }
            };

            IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();

            return new ClassLogic(config);
        }

        [Fact]
        public async Task SignUpMemberWhenClassIsFull_ReturnIsFull()
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                ClassLogic logic = CreateClassLogic();
                int testMemberId = 3; 
                int fullClassId = 6;  

                // Act
                MemberBookingResponse? result = await logic.SignUpAMember(testMemberId, fullClassId);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("ClassFull", result.Status);

            }
        }

        [Fact]
        public async Task SignUpMemberWhenClassIsNotFull_ReturnIsSuccess()
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                ClassLogic logic = CreateClassLogic();
                int testMemberId = 5;
                int notFullClassId = 1; 

                // Act
                MemberBookingResponse? result = await logic.SignUpAMember(testMemberId, notFullClassId);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Success", result.Status);
            }
        }

        [Fact]
        public async Task SignUpMemberWhenMemberIsAlreadySignedUp_ReturnAlreadySignedUp()
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                ClassLogic logic = CreateClassLogic();
                int testMemberId = 3;
                int classId = 1;

                // Act – prøv igen
                MemberBookingResponse? result = await logic.SignUpAMember(testMemberId, classId);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("AlreadySignedUp", result.Status);
            }
        }

        [Fact]
        public async Task GetAllClassesAsync_ReturnsListOfClasses()
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                ClassLogic logic = CreateClassLogic();

                // Act
                IEnumerable<Class>? result = await logic.GetAllClassesAsync();

                // Assert
                Assert.NotNull(result);
                Assert.NotEmpty(result);
            }
        }

    }
}
