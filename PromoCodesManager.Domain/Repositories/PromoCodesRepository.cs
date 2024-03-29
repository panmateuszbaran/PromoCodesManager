using PromoCodesManager.Domain.Entities;

namespace PromoCodesManager.Domain.Repositories
{    
    public interface IPromoCodesRepository : IRepository<PromoCode>
    {
        PromoCode GetByCode(string code);
    }

    public class PromoCodesRepository : IPromoCodesRepository
    {
        //To move to actual DB
        private static List<PromoCode> _promoCodes = new List<PromoCode>
        {
            new PromoCode
            {
                Code = "promo-1",
                IsActive = true,
                Name = "Promo number 1",
                UseLimit = 100
            },
            new PromoCode
            {
                Code = "promo-2",
                IsActive = false,
                Name = "Promo number 2",
                UseLimit = 10
            },
            new PromoCode
            {
                Code = "promo-3",
                IsActive = true,
                Name = "Super promo",
                UseLimit = 5
            }
        };

        public PromoCode Add(PromoCode entity)
        {
            _promoCodes.Add(entity);
            return entity;
        }

        public bool Delete(PromoCode entity)
        {
            if (!_promoCodes.Any(x => x.Code == entity.Code))
                return false;
            _promoCodes.Remove(entity);
            //Return false upon failure on DB
            return true;
        }

        public PromoCode[] GetAll()
        {
            return _promoCodes.ToArray();
        }

        public PromoCode GetByCode(string code)
        {
            return _promoCodes.Find(x => x.Code == code);
        }

        public async Task<int> SaveChangesAsync()
        {
            //Return actual number of changes from DB
            return 1;
        }

        public PromoCode Update(PromoCode entity)
        {
            var promoCode = _promoCodes.Find(x => x.Code == entity.Code);
            if (promoCode != null)
            {
                promoCode = entity;
            }

            return promoCode;
        }
    }
}
