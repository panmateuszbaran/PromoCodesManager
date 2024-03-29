using MediatR;
using Microsoft.AspNetCore.Mvc;
using PromoCodesManager.Business.Commands;
using PromoCodesManager.Business.Queries;
using PromoCodesManager.Domain.Entities;

namespace PromoCodesManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromoCodesController : ControllerBase
    {
        private readonly ILogger<PromoCodesController> _logger;
        private readonly ISender _mediator;

        public PromoCodesController(ILogger<PromoCodesController> logger, ISender mediatr)
        {
            _logger = logger;
            _mediator = mediatr;
        }

        [HttpGet("get-all")]
        public async Task<IEnumerable<PromoCode>> GetAll()
        {
            return await _mediator.Send(new GetAllPromoCodesQuery());
        }

        [HttpGet("get-by-code")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var promoCode = await _mediator.Send(new GetPromoCodeQuery(code));
            if (promoCode == null || promoCode.UseLimit == 0)
                return NotFound();
            promoCode.UseLimit--;
            await _mediator.Send(new UpdatePromoCodeCommand(promoCode));
            return Ok(promoCode);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(string name, string code, int useLimit)
        {
            var result = await _mediator.Send(new AddPromoCodeCommand
            {
                Name = name,
                Code = code,
                UseLimit = useLimit
            });
            return result ? Ok() : BadRequest();
        }

        [HttpPost("update-name")]
        public async Task<IActionResult> UpdateName(string code, string name)
        {
            var promoCode = await _mediator.Send(new GetPromoCodeQuery(code));
            if (promoCode == null)
                return NotFound();
            promoCode.Name = name;

            var result = await _mediator.Send(new UpdatePromoCodeCommand(promoCode));
            return Ok(result);
        }

        [HttpPost("set-as-inactive")]
        public async Task<IActionResult> SetAsInactive(string code)
        {
            var promoCode = await _mediator.Send(new GetPromoCodeQuery(code));
            if (promoCode == null)
                return NotFound();
            promoCode.IsActive = false;

            var result = await _mediator.Send(new UpdatePromoCodeCommand(promoCode));
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string code)
        {
            var result = await _mediator.Send(new DeletePromoCodeCommand(code));
            return Ok(result);
        }
    }
}
