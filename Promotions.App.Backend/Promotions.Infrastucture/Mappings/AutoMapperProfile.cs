using AutoMapper;
using Promotions.Core.DTOs;
using Promotions.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Promotions.Infrastucture.Mappings
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Promotion, PromotionDTO>();
            CreateMap< PromotionDTO, Promotion>();

        }
    }
}
