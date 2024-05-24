using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPaymentService
    {
        Task<(bool status, string message, PaymentDto? result)> CreatePayment(PaymentDto payment);

    }
}
