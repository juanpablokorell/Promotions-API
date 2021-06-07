using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Promotions.Core.DTOs;
using Promotions.Core.Entities;
using Promotions.Core.Interfaces;
using Promotions.Core.Services;

namespace Promotions.API.UseCases
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdatePromotionController : ControllerBase
    {
        private readonly IPromotionService _promotions;
        private readonly IMapper _mapper;

        public UpdatePromotionController(IPromotionService promotions, IMapper mapper)
        {
            _promotions = promotions;
            _mapper = mapper;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePromotion([FromBody] PromotionDTO promotion, string id)
        {
           
            promotion.Id = new Guid(id);

            var promotions = _mapper.Map<Promotion>(promotion);
            await _promotions.UpdatePromotion(promotions);
            return Ok(promotions);


        }
    }
}