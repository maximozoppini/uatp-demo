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
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        public CardService(IUserRepository userRepository, ICardRepository cardRepository, IMapper mapper) {
            _cardRepository = cardRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<(bool status, string message, CardDto? model)> AddCard(CardDto cardDto)
        {
            var user = await _userRepository.GetByName(cardDto.UserName);
            if (user == null) {
                return (false, "User doesn´t exist", null);
            }

            var card = _mapper.Map<CardDto,CreditCard>(cardDto);

            card.UserId = user.Id;

            try
            {
                await _cardRepository.Add(card);
                return (true, "Credit card successfully created", cardDto);
            }
            catch (Exception e)
            {
                return (false, e.Message, null);
            }

        }

        public async Task<(bool status, string message, CardBalanceDto? model)> GetCardByNumber(string cardNumber)
        {
            var card = await _cardRepository.FindByNumber(cardNumber);

            if (card == null)
            {
                return (false, "Invalid card number", null);
            }

            return (true, "", _mapper.Map<CardBalanceDto>(card));
        }
    }
}
