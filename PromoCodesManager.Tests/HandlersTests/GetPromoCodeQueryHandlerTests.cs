using Moq;
using PromoCodesManager.Business.Queries;
using PromoCodesManager.Domain.Entities;
using PromoCodesManager.Domain.Repositories;

namespace PromoCodesManager.Tests.HandlersTests
{
    public class GetPromoCodeQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ValidPromoCode_ReturnsPromoCode()
        {
            // Arrange
            var promoCode = new PromoCode { Code = "ABC123" };
            var repositoryMock = new Mock<IPromoCodesRepository>();
            repositoryMock.Setup(r => r.GetByCode("ABC123")).Returns(promoCode);
            var handler = new GetPromoCodeQueryHandler(repositoryMock.Object);

            // Act
            var result = await handler.Handle(new GetPromoCodeQuery("ABC123"), CancellationToken.None);

            // Assert
            Assert.Equal(promoCode, result);
        }

        [Fact]
        public async Task Handle_InvalidPromoCode_ReturnsNull()
        {
            // Arrange
            var repositoryMock = new Mock<IPromoCodesRepository>();
            repositoryMock.Setup(r => r.GetByCode("XYZ789")).Returns(value: null);
            var handler = new GetPromoCodeQueryHandler(repositoryMock.Object);

            // Act
            var result = await handler.Handle(new GetPromoCodeQuery("XYZ789"), CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}