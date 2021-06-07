using Promotions.Core.DTOs;
using Promotions.Core.Entities;
using Promotions.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Promotions.Core.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;
     
        public PromotionService(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        public async Task DeletePromotion(string id)
        {
            await _promotionRepository.DeletePromotion(id);

        }

        public void Error()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Promotion>> GetAllPromotion()
        {
          return  await _promotionRepository.GetAllPromotion();

        }

        public async Task<List<Promotion>> GetCurrentPromotion()
        {
          return  await _promotionRepository.GetCurrentPromotion();

        }

        public async Task<List<Promotion>> GetCurrentPromotionBySale(string PaymentMethods, string Bank, IEnumerable<string> ProductCategories)
        {
            return await _promotionRepository.GetCurrentPromotionBySale(PaymentMethods,Bank,ProductCategories);

        }

        public async Task<List<Promotion>> GetCurrentPromotionByDate(DateTime fecha)
        {
           return await _promotionRepository.GetCurrentPromotionByDate(fecha);
           
        }

        public async Task<Promotion> GetPromotionById(string id)
        {
            return await _promotionRepository.GetPromotionById(id);

        }

        public async Task InsertPromotion(Promotion promotions)
        {
          
            await _promotionRepository.InsertPromotion(promotions);
        
         
        }

       
        public async Task UpdatePromotion(Promotion promotions)
        {
            await _promotionRepository.UpdatePromotion(promotions);

        }

    }
}
