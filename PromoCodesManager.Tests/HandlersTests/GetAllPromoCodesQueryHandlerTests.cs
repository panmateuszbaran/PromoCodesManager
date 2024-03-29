using Moq;
using PromoCodesManager.Business.Queries;
using PromoCodesManager.Domain.Entities;
using PromoCodesManager.Domain.Repositories;

namespace PromoCodesManager.Tests.HandlersTests
{
    public class GetAllPromoCodesQueryHandlerTests
    {

        [Fact]
        public async Task GetAllPromoCodesQuery_ReturnsAllPromoCodes()
        {
            // Arrange
            var promoCodes = new List<PromoCode>
            {
                new PromoCode { Code = "ABC123" },
                new PromoCode { Code = "XYZ789" }
            };

            var repositoryMock = new Mock<IPromoCodesRepository>();
            repositoryMock.Setup(r => r.GetAll()).Returns(promoCodes.ToArray);
            var handler = new GetAllPromoCodesQueryHandler(repositoryMock.Object);

            var query = new GetAllPromoCodesQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count()); // Check if all promo codes are returned
            Assert.Contains(promoCodes[0], result); // Check if specific promo codes are in the result
            Assert.Contains(promoCodes[1], result);
        }
    }
}