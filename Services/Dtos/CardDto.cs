using Services.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dtos
{
    public class CardDto
    {
        [Required]
        [CreditCardNumber]
        public string Number { get; set; }

        [Required]
        [RegularExpression("(^[0-9]{3,4}$)", ErrorMessage = "Please enter a valid card code")]
        public string CardCode { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public string UserName { get; set; }


    }
}
