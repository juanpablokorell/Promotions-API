using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Promotions.Core.DTOs;
using Promotions.Core.Entities;
using Promotions.Core.Interfaces;
using Promotions.Core.Services;
using Promotions.Infrastucture.Repositories;

namespace Promotions.API.UseCases
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly IPromotionService _promotionService;
        private readonly IMapper _mapper;

        public PromotionsController(IPromotionService promotionService, IMapper mapper)
        {
            _promotionService = promotionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPromotions()
        {
            var promotions = await _promotionService.GetAllPromotion();
            var promotionDTO = _mapper.Map<IEnumerable<PromotionDTO>>(promotions);
            return Ok(promotionDTO);

        }

        [HttpGet("GetPromotionsDetails/{id}")]
        public async Task<IActionResult> GetPromotionsDetails(string id)
        {
            var promotions = await _promotionService.GetPromotionById(id);
            var promotionDTO = _mapper.Map<PromotionDTO>(promotions);
            return Ok(promotionDTO);

        }

        [HttpGet("GetCurrentPromotionsBySale")]

        public async Task<IActionResult> GetCurrentPromotionsBySale([FromQuery]string PaymentMethods, [FromQuery]string Bank, [FromQuery] IEnumerable<string> ProductCategories)
        {
            var promotions = await _promotionService.GetCurrentPromotionBySale(PaymentMethods, Bank, ProductCategories);
            var promotionDTO = _mapper.Map<IEnumerable<PromotionDTO>>(promotions);
            return Ok(promotionDTO);

        }

        [HttpGet("GetCurrentPromotions")]
        public async Task<IActionResult> GetCurrentPromotions()
        {
            var promotions = await _promotionService.GetCurrentPromotion();
            var promotionDTO = _mapper.Map<IEnumerable<PromotionDTO>>(promotions);
            return Ok(promotionDTO);

        }

        [HttpGet("GetCurrentPromotionsByDate/{fecha}")]

        public async Task<IActionResult> GetCurrentPromotionsByDate(DateTime fecha)
        {
            var promotions = await _promotionService.GetCurrentPromotionByDate(fecha);
            var promotionDTO = _mapper.Map<IEnumerable<PromotionDTO>>(promotions);
            return Ok(promotionDTO);

        }
        [HttpPost("CreatePromotion")]
        public async Task<IActionResult> CreatePromotion([FromBody] PromotionDTO promotion)
        {

            if (promotion == null)
                return BadRequest();

            var promotions = _mapper.Map<Promotion>(promotion);
            await _promotionService.InsertPromotion(promotions);
            // return Ok(promotions);
            return Created("Created", true);


        }


        [HttpDelete("DeletePromotion/{id}")]
        public async Task<ActionResult> DeletePromotion(string id)
        {
            await _promotionService.DeletePromotion(id);
            return NoContent();
        }

        [HttpPut("UpdatePromotion/{id}")]
        public async Task<IActionResult> UpdatePromotion([FromBody] PromotionDTO promotion, string id)
        {

            promotion.Id = new Guid(id);

            var promotions = _mapper.Map<Promotion>(promotion);
            await _promotionService.UpdatePromotion(promotions);
            // return Ok(promotions);
            return Created("Updated", true);

        }

        [HttpPut("UpdatePromotionValidy")]
        public async Task<IActionResult> UpdatePromotionValidy([FromQuery] string id, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var reg = await _promotionService.GetPromotionById(id);
            var pdto = _mapper.Map<PromotionDTO>(reg);

            pdto.StartDate = startDate;
            pdto.EndDate = endDate;

            var promotions = _mapper.Map<Promotion>(pdto);
            await _promotionService.UpdatePromotion(promotions);
            var pdtoAct = _mapper.Map<PromotionDTO>(promotions);
            return Created("Updated", true);

        }
    }
}