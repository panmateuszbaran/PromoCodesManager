using Moq;
using PromoCodesManager.Business.Commands;
using PromoCodesManager.Domain.Entities;
using PromoCodesManager.Domain.Repositories;

namespace PromoCodesManager.Tests.HandlersTests
{
    public class AddPromoCodeCommandHandlerTests
    {
        [Fact]
        public async Task AddPromoCodeCommand_ReturnsTrue()
        {
            // Arrange
            var promoCode = new PromoCode { Name = "TEST1", Code = "NEW123" };

            var repositoryMock = new Mock<IPromoCodesRepository>();
            repositoryMock.Setup(r => r.Add(promoCode)).Returns(promoCode);
            repositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);
            var handler = new AddPromoCodeCommandHandler(repositoryMock.Object);

            var command = new AddPromoCodeCommand
            {
                Name = promoCode.Name,
                Code = promoCode.Code
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AddPromoCodeCommand_ReturnsFalse()
        {
            // Arrange
            var promoCode = new PromoCode { Name = "DUPLICATE", Code = "NEW123" };

            var repositoryMock = new Mock<IPromoCodesRepository>();
            repositoryMock.Setup(r => r.Add(promoCode)).Returns(promoCode);
            repositoryMock.Setup(r => r.GetByCode("NEW123")).Returns(promoCode);
            repositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);
            var handler = new AddPromoCodeCommandHandler(repositoryMock.Object);

            var command = new AddPromoCodeCommand
            {  
                Name = promoCode.Name,
                Code = promoCode.Code
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
        }
    }
}