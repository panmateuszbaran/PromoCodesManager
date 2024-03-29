using PromoCodesManager.Domain.Entities;
using PromoCodesManager.Domain.Repositories;

namespace PromoCodesManager.Tests.RepositoriesTests
{
    public class PromoCodesRepositoryTests
    {
        [Fact]
        public void GetPromoCodeByCode_ReturnsPromoCode()
        {
            // Arrange
            var promoCode = new PromoCode { Code = "ABC123" };
            var repository = new PromoCodesRepository();  // Assuming PromoCodesRepository has no external dependencies
            repository.Add(promoCode);

            // Act
            var result = repository.GetByCode("ABC123");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(promoCode, result);
        }

        [Fact]
        public void AddPromoCode_ReturnsTrue()
        {
            // Arrange
            var promoCode = new PromoCode { Code = "NEW123" };
            var repository = new PromoCodesRepository();  // Assuming PromoCodesRepository has no external dependencies

            // Act
            var result = repository.Add(promoCode);

            // Assert
            Assert.Equal(promoCode, result);
        }

        [Fact]
        public void DeletePromoCode_ReturnsTrue()
        {
            // Arrange
            var promoCode = new PromoCode { Code = "promo-1" };
            var repository = new PromoCodesRepository();  // Assuming PromoCodesRepository has no external dependencies

            // Act
            var result = repository.Delete(promoCode);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DeletePromoCode_ReturnsFalseForNonExisting()
        {
            // Arrange
            var promoCode = new PromoCode { Code = "NONEXISTING" };
            var repository = new PromoCodesRepository();  // Assuming PromoCodesRepository has no external dependencies

            // Act
            var result = repository.Delete(promoCode);

            // Assert
            Assert.False(result);
        }
    }
}
