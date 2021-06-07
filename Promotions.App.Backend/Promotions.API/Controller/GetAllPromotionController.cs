using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Promotions.Core.DTOs;
using Promotions.Core.Interfaces;
using Promotions.Core.Services;

namespace Promotions.API.UseCases
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAllPromotionController : ControllerBase
    {
        private readonly IPromotionService _promotions;
        private readonly IMapper _mapper;
        public GetAllPromotionController(IPromotionService promotions, IMapper mapper)
        {
            _promotions = promotions;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPromotions()
        {
            var promotions = await _promotions.GetAllPromotion();
            var promotionDTO = _mapper.Map<IEnumerable<PromotionDTO>>(promotions);

            return Ok(promotionDTO);

        }
    }
}