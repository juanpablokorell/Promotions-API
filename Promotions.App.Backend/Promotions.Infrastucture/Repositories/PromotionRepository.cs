using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Promotions.Core.Entities;
using Promotions.Core.Interfaces;

using Promotions.Infrastucture.Interfaces;

namespace Promotions.Infrastucture.Repositories
{
   public class PromotionRepository : IPromotionRepository
    {
        protected readonly IMongoContext Context;
        private IMongoCollection<Promotion> Collection;
        // config 
        public PromotionRepository(IMongoContext context)
        {
            Context = context;

            Collection = context.GetCollection<Promotion>("Promotion");

        }


        public async Task DeletePromotion(string id)
        {
            var fillter = Builders<Promotion>.Filter.Eq(s => s.Id, new Guid(id));

            var promotion =  await Collection.FindAsync(
               fillter).Result.
                FirstOrDefaultAsync();

            promotion.Active = false;
            promotion.ModificationDate = DateTime.Now;

            await Collection.ReplaceOneAsync(fillter, promotion);
        
        }

        public async Task<List<Promotion>> GetAllPromotion()
        {
            var filter = Builders<Promotion>
               .Filter
               .Where(s =>s.Active == true);
            return await Collection.FindAsync(filter).Result.ToListAsync();
        }

        public async Task<List<Promotion>> GetCurrentPromotion()
        {
            var filter = Builders<Promotion>
                 .Filter
                   .Where(s => s.EndDate >= DateTime.Now.Date && s.StartDate <= DateTime.Now.Date && s.Active == true);
            return await Collection.FindAsync(
              filter
              ).Result.ToListAsync();



        }

        public  async Task<List<Promotion>> GetCurrentPromotionBySale(string PaymentMethods, string Bank, IEnumerable<string> ProductCategories)
        {
            var filter = Builders<Promotion>
                 .Filter
                 .Where(s => s.EndDate >= DateTime.Now.Date && s.StartDate <= DateTime.Now.Date && s.Active == true
                  && s.PaymentMethods.Contains(PaymentMethods)
                  && s.Banks.Contains(Bank)
                  && s.ProductCategories.Equals(ProductCategories)

                  );
            return await Collection.FindAsync(
              filter
              ).Result.ToListAsync();
        }

    

        public async Task<List<Promotion>> GetCurrentPromotionByDate(DateTime fecha)
        {

            var filter = Builders<Promotion>
                .Filter
                .Where(s => s.EndDate >=fecha.Date && s.StartDate<=fecha.Date && s.Active==true);
            return await Collection.FindAsync(
              filter
              ).Result.ToListAsync();


        }

        public async Task<Promotion> GetPromotionById(string id)
        {
            var fillter = Builders<Promotion>.Filter
                .Where(s => s.Active == true && s.Id == new Guid(id));
                //.Eq(s => s.Id, new Guid(id));

            return await Collection.FindAsync(
               fillter).Result.
                FirstOrDefaultAsync();
        }

        public async Task InsertPromotion(Promotion promotions)
        {
            promotions.Active = true;
            promotions.CreationDate = DateTime.Now;
            await Collection.InsertOneAsync(promotions);
        }

        public async Task UpdatePromotion(Promotion promotions)
        {
            promotions.ModificationDate = DateTime.Now;
            promotions.Active = true;
            var filter = Builders<Promotion>
                .Filter
                .Eq(s => s.Id, promotions.Id);
            await Collection.ReplaceOneAsync(filter, promotions);
        }

      



    }
}
