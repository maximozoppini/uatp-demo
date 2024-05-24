using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CreditCard : BaseEntity
    {
        public string Number { get; set; }
        public decimal Balance { get; set; }
        public string CardCode { get; set; }
        public DateTime ExpirationDate { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}
