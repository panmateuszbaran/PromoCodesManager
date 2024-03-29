using MediatR;
using PromoCodesManager.Domain.Entities;
using PromoCodesManager.Domain.Repositories;

namespace PromoCodesManager.Business.Commands
{
    public class AddPromoCodeCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int UseLimit { get; set; }
    }

    public class AddPromoCodeCommandHandler : IRequestHandler<AddPromoCodeCommand, bool>
    {
        private readonly IPromoCodesRepository _repository;

        public AddPromoCodeCommandHandler(IPromoCodesRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(AddPromoCodeCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var existingPromoCode = _repository.GetByCode(request.Code);

            if (existingPromoCode != null)
                return false;

            var promoCode = new PromoCode
            {
                Name = request.Name,
                Code = request.Code,
                UseLimit = request.UseLimit,
                IsActive = true
            };

            _repository.Add(promoCode);

            var numberOfChanges = await _repository.SaveChangesAsync();

            if (numberOfChanges > 0)
                GlobalHistoryPlaceholder.History.Add(new HistoryEvent { EventDate = DateTime.Now, EventType = HistoryEventType.Created, PromoCode = promoCode });


            return numberOfChanges > 0;
        }
    }
}
