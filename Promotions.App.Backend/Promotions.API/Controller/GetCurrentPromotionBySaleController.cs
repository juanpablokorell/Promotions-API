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
    public class GetCurrentPromotionBySaleController : ControllerBase
    {
        private readonly IPromotionService _promotions;
        private readonly IMapper _mapper;

        public GetCurrentPromotionBySaleController(IPromotionService promotions, IMapper mapper)
        {
            _promotions = promotions;
            _mapper = mapper;
        }
        [HttpGet]

        public async Task<IActionResult> GetCurrentPromotionsBySale([FromQuery]string PaymentMethods, [FromQuery]string Bank, [FromQuery] IEnumerable<string> ProductCategories)
        {
            var promotions = await _promotions.GetCurrentPromotionBySale(PaymentMethods, Bank, ProductCategories);
            var promotionDTO = _mapper.Map<IEnumerable<PromotionDTO>>(promotions);
            return Ok(promotionDTO);

        }
    }
}