using MediatR;
using PromoCodesManager.Domain.Entities;
using PromoCodesManager.Domain.Repositories;

namespace PromoCodesManager.Business.Queries
{
    public class GetPromoCodeQuery: IRequest<PromoCode>
    {
        public string Code { get; set; }

        public GetPromoCodeQuery(string code) { Code = code; }
    }

    public class GetPromoCodeQueryHandler : IRequestHandler<GetPromoCodeQuery, PromoCode>
    {
        private readonly IPromoCodesRepository _repository;

        public GetPromoCodeQueryHandler(IPromoCodesRepository repository)
        {
            _repository = repository;
        }

        public async Task<PromoCode> Handle(GetPromoCodeQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var promoCode = _repository.GetByCode(request.Code);

            if (promoCode == null)
            {
                return null;
            }
            
            GlobalHistoryPlaceholder.History.Add(new HistoryEvent { EventDate = DateTime.Now, EventType = HistoryEventType.Used, PromoCode = promoCode });

            return promoCode;
        }
    }
}
