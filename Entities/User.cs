using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User : BaseEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public ICollection<CreditCard> CreditCards { get; set; }
    }
}
