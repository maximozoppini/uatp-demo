using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Payment : BaseEntity
    {
        [Required]
        public decimal Amount { get; set; }

        public decimal Fee { get; set; }

        [ForeignKey("CreditCard")]
        public int CreditCardId { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}
