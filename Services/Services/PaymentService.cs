using AutoMapper;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.Interfaces;
using Services.Dtos;
using Services.Interfaces;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        public PaymentService(IUserRepository userRepository, ICardRepository cardRepository
            , IMapper mapper, IPaymentRepository paymentRepository) {
            _cardRepository = cardRepository;
            _userRepository = userRepository;
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<(bool status, string message, PaymentDto? result)> CreatePayment(PaymentDto paymentDto)
        {
            var card = await _cardRepository.FindByNumber(paymentDto.CardNumber);

            if (card == null) {
                return (false, "Invalid card number", null);
            }

            if (card.ExpirationDate < DateTime.Now) {
                return (false, "Credit card has expired", null);
            }

            var payment = _mapper.Map<Payment> (paymentDto);
            var feeExchange = UniversalFeesExchange.Instance;

            payment.CreditCardId = card.Id;
            payment.Fee = feeExchange.GetCurrentFee();

            await _paymentRepository.Add(payment);
            
            card.Balance = card.Balance + payment.Amount + ( payment.Amount * feeExchange.GetCurrentFee());
            await _cardRepository.Update(card);

            return (true, "Payment created successfully", paymentDto);
        }
    }
}
