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
    public class GetPromotionsByIdController : ControllerBase
    {
        private readonly IPromotionService _promotions;
        private readonly IMapper _mapper;

        public GetPromotionsByIdController(IPromotionService promotions, IMapper mapper)
        {
            _promotions = promotions;
            _mapper = mapper;
        }

        [HttpGet("GetPromotionsDetails/{id}")]
        public async Task<IActionResult> GetPromotionsDetails(string id)
        {
            var promotions = await _promotions.GetPromotionById(id);
            var promotionDTO = _mapper.Map<PromotionDTO>(promotions);
            return Ok(promotionDTO);

        }
    }
}