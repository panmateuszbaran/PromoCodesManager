using MediatR;
using PromoCodesManager.Domain.Entities;
using PromoCodesManager.Domain.Repositories;

namespace PromoCodesManager.Business.Queries
{
    public class GetAllPromoCodesQuery : IRequest<IEnumerable<PromoCode>> { }

    public class GetAllPromoCodesQueryHandler : IRequestHandler<GetAllPromoCodesQuery, IEnumerable<PromoCode>>
    {
        private readonly IPromoCodesRepository _repository;

         public GetAllPromoCodesQueryHandler(IPromoCodesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PromoCode>> Handle(GetAllPromoCodesQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return _repository.GetAll();
        }
    }
}
