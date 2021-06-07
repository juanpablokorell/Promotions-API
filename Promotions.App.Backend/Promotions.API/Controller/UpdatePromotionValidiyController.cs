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
    public class UpdatePromotionValidityController : ControllerBase
    {
        private readonly IPromotionService _promotions;
        private readonly IMapper _mapper;

        public UpdatePromotionValidityController(IPromotionService promotions, IMapper mapper)
        {
            _promotions = promotions;
            _mapper = mapper;
        }

        [HttpPut()]
            public async Task<IActionResult> UpdatePromotionValidy([FromQuery] string id, [FromQuery] DateTime startDate,[FromQuery] DateTime endDate)
        {
            var reg = await _promotions.GetPromotionById(id);
            var pdto = _mapper.Map<PromotionDTO>(reg);

            pdto.StartDate = startDate;
            pdto.EndDate = endDate;

            var promotions = _mapper.Map<Promotion>(pdto);
            await _promotions.UpdatePromotion(promotions);
            var pdtoAct = _mapper.Map<PromotionDTO>(promotions);
            return Ok(pdtoAct);


        }
    }
}