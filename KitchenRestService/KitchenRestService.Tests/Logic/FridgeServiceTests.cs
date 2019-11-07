using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitchenRestService.Logic;
using Moq;
using Xunit;

namespace KitchenRestService.Tests.Logic
{
    public class FridgeServiceTests
    {
        [Fact]
        public async Task CleanFridgeDoesNothingOnEmptyFridge()
        {
            var mockRepo = new Mock<IKitchenRepo>();
            mockRepo.Setup(r => r.GetAllFridgeItemsAsync())
                .ReturnsAsync(Enumerable.Empty<FridgeItem>());

            var service = new FridgeService(mockRepo.Object);

            var cleaned = await service.CleanFridgeAsync();

            Assert.False(cleaned);
        }
    }
}
