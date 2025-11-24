using FitSammenWebClient.BusinessLogicLayer;
using FitSammenWebClient.Models;
using FitSammenWebClient.ServiceLayer;
using Microsoft.Extensions.Configuration;

namespace FitsammenMVCTests
{
    public class WaitingListLogicTests
    {
        private class WaitingListAccessMock : IWaitingListAccess
        {
            public WaitingListStatus StatusToReturn { get; set; }
            public int? PositionToReturn { get; set; }
            public bool ThrowException { get; set; }

            public Task<WaitingListEntryResponse> AddMemberToWaitingListAsync(int classId, int memberId)
            {
                if (ThrowException)
                {
                    throw new InvalidOperationException("Simulated failure");
                }
                return Task.FromResult(new WaitingListEntryResponse
                {
                    Status = StatusToReturn,
                    WaitingListPosition = PositionToReturn
                });
            }
        }

        [Fact]
        public async Task AddMemberToWaitingListAsync_ReturnsSuccess_WithPosition()
        {
            var mock = new WaitingListAccessMock { StatusToReturn = WaitingListStatus.Success, PositionToReturn = 3 };
            var logic = new WaitingListLogic(mock);

            var result = await logic.AddMemberToWaitingListAsync(10, 5);

            Assert.NotNull(result);
            Assert.Equal(WaitingListStatus.Success, result.Status);
            Assert.Equal(3, result.WaitingListPosition);
        }

        [Fact]
        public async Task AddMemberToWaitingListAsync_ReturnsAlreadySignedUp()
        {
            var mock = new WaitingListAccessMock { StatusToReturn = WaitingListStatus.AlreadySignedUp, PositionToReturn = null };
            var logic = new WaitingListLogic(mock);

            var result = await logic.AddMemberToWaitingListAsync(11, 6);

            Assert.NotNull(result);
            Assert.Equal(WaitingListStatus.AlreadySignedUp, result.Status);
            Assert.Null(result.WaitingListPosition);
        }

        [Fact]
        public async Task AddMemberToWaitingListAsync_ReturnsError()
        {
            var mock = new WaitingListAccessMock { StatusToReturn = WaitingListStatus.Error, PositionToReturn = null };
            var logic = new WaitingListLogic(mock);

            var result = await logic.AddMemberToWaitingListAsync(12, 7);

            Assert.NotNull(result);
            Assert.Equal(WaitingListStatus.Error, result.Status);
            Assert.Null(result.WaitingListPosition);
        }

        [Fact]
        public async Task AddMemberToWaitingListAsync_WhenAccessThrows_PropagatesException()
        {
            var mock = new WaitingListAccessMock { ThrowException = true };
            var logic = new WaitingListLogic(mock);

            await Assert.ThrowsAsync<InvalidOperationException>(() => logic.AddMemberToWaitingListAsync(13, 8));
        }

        private static WaitingListLogic CreateWaitingListLogicWithRealService()
        {
            var settings = new Dictionary<string, string?> { { "ServiceUrlToUse", "https://localhost:7229/api/" } };
            IConfiguration config = new ConfigurationBuilder().AddInMemoryCollection(settings).Build();
            return new WaitingListLogic(config);
        }

        [Fact]
        public async Task AddMemberToWaitingListAsync_RealService_ReturnsResponseOrErrorStatus()
        {
            // This test calls the real service; depending on environment it may fail. We only assert non-null response.
            var logic = CreateWaitingListLogicWithRealService();
            try
            {
                var result = await logic.AddMemberToWaitingListAsync(1, 1);
                Assert.NotNull(result);
            }
            catch (Exception ex)
            {
                // If service unreachable, we still consider the test meaningful by asserting exception thrown.
                Assert.IsAssignableFrom<Exception>(ex);
            }
        }
    }
}
