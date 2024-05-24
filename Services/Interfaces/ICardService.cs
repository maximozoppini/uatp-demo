using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICardService
    {
        Task<(bool status, string message, CardDto? model)> AddCard(CardDto model);
        Task<(bool status, string message, CardBalanceDto? model)> GetCardByNumber(string cardNumber);
    }
}
