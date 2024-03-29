using MediatR;
using PromoCodesManager.Domain.Entities;
using PromoCodesManager.Domain.Repositories;

namespace PromoCodesManager.Business.Commands
{
    public class UpdatePromoCodeCommand : IRequest<bool>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int UseLimit { get; set; }

        public UpdatePromoCodeCommand(PromoCode promoCode)
        {
            Code = promoCode.Code;
            Name = promoCode.Name;
            UseLimit = promoCode.UseLimit;
        }
    }

    public class UpdatePromoCodeCommandHandler : IRequestHandler<UpdatePromoCodeCommand, bool>
    {
        private readonly IPromoCodesRepository _repository;

        public UpdatePromoCodeCommandHandler(IPromoCodesRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdatePromoCodeCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var promoCode = _repository.GetByCode(request.Code);

            if (promoCode == null)
                return false;

            promoCode.UseLimit = request.UseLimit;
            promoCode.Name = request.Name;

            _repository.Update(promoCode);

            var numberOfChanges = await _repository.SaveChangesAsync();

            if (numberOfChanges > 0)
                GlobalHistoryPlaceholder.History.Add(new HistoryEvent { EventDate = DateTime.Now, EventType = HistoryEventType.Updated, PromoCode = promoCode });

            return numberOfChanges > 0;
        }
    }
}
