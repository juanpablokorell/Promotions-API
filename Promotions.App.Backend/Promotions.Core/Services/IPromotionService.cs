using Promotions.Core.DTOs;
using Promotions.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Promotions.Core.Services
{
    public interface IPromotionService
    {
        Task InsertPromotion(Promotion promotions);
        Task UpdatePromotion(Promotion promotions);
        Task DeletePromotion(string id);

        Task<List<Promotion>> GetAllPromotion();
        Task<Promotion> GetPromotionById(string id);
        Task<List<Promotion>> GetCurrentPromotion();
        Task<List<Promotion>> GetCurrentPromotionByDate(DateTime fecha);
        Task<List<Promotion>> GetCurrentPromotionBySale(string PaymentMethods, string Bank, IEnumerable<string> ProductCategories);


    }
}