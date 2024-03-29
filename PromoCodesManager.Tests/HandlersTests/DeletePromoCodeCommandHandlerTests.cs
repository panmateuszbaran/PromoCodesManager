using Moq;
using PromoCodesManager.Business.Commands;
using PromoCodesManager.Domain.Entities;
using PromoCodesManager.Domain.Repositories;

namespace PromoCodesManager.Tests.HandlersTests
{
    public class DeletePromoCodeCommandHandlerTests
    {
        [Fact]
        public async Task DeletePromoCodeCommand_NotExisting_ReturnsFalse()
        {
            // Arrange
            var promoCode = new PromoCode { Name = "TEST1", Code = "NEW123" };

            var repositoryMock = new Mock<IPromoCodesRepository>();
            repositoryMock.Setup(r => r.GetByCode(promoCode.Code)).Returns(value: null);
            repositoryMock.Setup(r => r.Delete(promoCode)).Returns(true);
            var handler = new DeletePromoCodeCommandHandler(repositoryMock.Object);

            var command = new DeletePromoCodeCommand(promoCode);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeletePromoCodeCommand_Existing_ReturnsTrue()
        {
            // Arrange
            var promoCode = new PromoCode { Name = "TEST1", Code = "NEW123" };

            var repositoryMock = new Mock<IPromoCodesRepository>();
            repositoryMock.Setup(r => r.GetByCode(promoCode.Code)).Returns(promoCode);
            repositoryMock.Setup(r => r.Delete(promoCode)).Returns(true);
            var handler = new DeletePromoCodeCommandHandler(repositoryMock.Object);

            var command = new DeletePromoCodeCommand(promoCode);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
        }
    }
}