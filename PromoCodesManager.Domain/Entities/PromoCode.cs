
namespace PromoCodesManager.Domain.Entities
{
    public class PromoCode : IEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int UseLimit { get; set; }
        public bool IsActive { get; set; }
    }
}
