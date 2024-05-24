using Entities;
using Entities.Context;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CardRepository : Repository<CreditCard, int>, ICardRepository
    {
        public CardRepository(RapidPayContext context)
            : base(context) { }

        public async Task<CreditCard> FindByNumber(string cardNumber)
        {
            return await _context.CreditCards.FirstOrDefaultAsync(c => c.Number == cardNumber);
        }

    }
}
