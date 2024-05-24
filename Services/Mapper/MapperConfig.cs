using AutoMapper;
using Entities;
using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapper
{
    public class MapperConfig: Profile
    {
        public MapperConfig()
        {
            CreateMap<UserRegistrationDto, User>();
            CreateMap<CardDto, CreditCard>();
            CreateMap<CreditCard, CardBalanceDto>();
            CreateMap<PaymentDto, Payment>();

        }
    }
}
