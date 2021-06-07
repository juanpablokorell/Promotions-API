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
    public class CreatePromotionController : ControllerBase
    {
        private readonly IPromotionService _promotions;
        private readonly IMapper _mapper;
        private IActionResult? _viewModel;

        public CreatePromotionController(IPromotionService promotions, IMapper mapper)
        {
            _promotions = promotions;
            _mapper = mapper;
        }

    
        [HttpPost]
        public async Task<IActionResult> CreatePromotion([FromBody] PromotionDTO promotion )
        {
        
            if (promotion == null)
                return BadRequest();


            var promotions = _mapper.Map<Promotion>(promotion);
            await _promotions.InsertPromotion(promotions);
            //return Ok(promotions);
            return _viewModel;


        }

    }
}