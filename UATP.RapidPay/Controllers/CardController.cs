using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;
using System.Net;

namespace UATP.RapidPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly IPaymentService _paymentService;
        public CardController(ICardService cardService, IPaymentService paymentService)
        {
            _cardService = cardService;
            _paymentService = paymentService;
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCard([FromBody] CardDto cardDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid card attributes");

                var (status, message, card) = await _cardService.AddCard(cardDto);

                if (!status)
                {
                    return BadRequest(new {success = status, errors = message});
                }
                return CreatedAtAction(nameof(CreateCard), card);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("Pay")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePayment(PaymentDto payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid payment attributes");
            }

            var (status, message, result) = await _paymentService.CreatePayment(payment);

            if (!status)
            {
                return BadRequest(new { success = status, errors = message });
            }
            return CreatedAtAction(nameof(CreatePayment), result);
        }

        [HttpGet]
        [Route("GetBalance")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBalance(string cardNumber)
        {
            var (status, message, result) = await _cardService.GetCardByNumber(cardNumber);
            if (!status)
            {
                return BadRequest(new { success = status, errors = message });
            }
            return Ok(result);
        }

    }
}
