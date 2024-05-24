using Services.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dtos
{
    public class CardBalanceDto
    {
        public string Number { get; set; }

        public string CardCode { get; set; }

        public DateTime ExpirationDate { get; set; }

        public decimal Balance { get; set; }

    }
}
