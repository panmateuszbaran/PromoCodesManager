using MediatR;
using PromoCodesManager.Domain.Entities;
using PromoCodesManager.Domain.Repositories;

namespace PromoCodesManager.Business.Commands
{
    public class DeletePromoCodeCommand : IRequest<bool>
    {
        public string Code { get; set; }

        public DeletePromoCodeCommand(string code)
        {
            Code = code;
        }

        public DeletePromoCodeCommand(PromoCode promoCode)
        {
            Code = promoCode.Code;
        }
    }

    public class DeletePromoCodeCommandHandler : IRequestHandler<DeletePromoCodeCommand, bool>
    {
        private readonly IPromoCodesRepository _repository;

        public DeletePromoCodeCommandHandler(IPromoCodesRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeletePromoCodeCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var promoCode = _repository.GetByCode(request.Code);

            if (promoCode == null)
                return false;

            GlobalHistoryPlaceholder.History.Add(new HistoryEvent { EventDate = DateTime.Now, EventType = HistoryEventType.Deleted, PromoCode = promoCode });

            return _repository.Delete(promoCode);
        }
    }
}
