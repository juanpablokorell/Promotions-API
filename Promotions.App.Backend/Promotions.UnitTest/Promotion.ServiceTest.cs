using AutoMapper;
using Moq;
using Promotions.Core.DTOs;
using Promotions.Core.Entities;
using Promotions.Core.Interfaces;
using Promotions.Core.Services;
using Promotions.Infrastucture.Mappings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Xunit;

namespace Promotions.UnitTest
{
    public class PromotionServiceTest
    {

        private readonly PromotionService _promServ;
        private readonly Mock<IPromotionRepository> _promotrionRepositoryMock = new Mock<IPromotionRepository>();
        

        public PromotionServiceTest()
        {
            _promServ = new PromotionService(_promotrionRepositoryMock.Object);
            
        }
        #region Region 1 Test Method GetPromotionByID

        [Fact]
        public async Task GetPromotionByIDTest() // positive
        {
            //Arrange
            var promotionID = Guid.NewGuid();
            var promotioDTO = new Promotion
            {
                Id = promotionID,
                PaymentMethods = new string[] { "TARJETA_CREDITO", "TARJETA_DEBITO", "EFECTIVO", "GIFT_CARD" },
                Banks = new string[] { "SANTANDER RIO", "ICBC" },
                ProductCategories = new string[] { "Hogar", "Jardin", "ElectroCocina", "GrandesElectro", "Colchones", "Celulares", "Tecnologia", "Audio" },
                MaximumAmountInstallments = 12,
                InterestValueFees = 0,
                DiscountPercentage = null,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(60)
            };
    
            _promotrionRepositoryMock.Setup(x=>x.GetPromotionById(promotionID.ToString())).
                ReturnsAsync(promotioDTO);

            //Act
            Promotion promotion = await _promServ.GetPromotionById(promotionID.ToString());
            //Asert
            Assert.Equal(promotionID.ToString(),promotion.Id.ToString());
        }


        [Fact]
        public async Task GetPromotionByIDNotExistTest() // negative
        {
            //Arrange
            var promotionID = Guid.NewGuid();
           
            _promotrionRepositoryMock.Setup(x => x.GetPromotionById(It.IsAny<Guid>().ToString())).
                ReturnsAsync(()=>null);

            //Act
            Promotion promotion = await _promServ.GetPromotionById(Guid.NewGuid().ToString());
            //Asert
            Assert.Null(promotion);
        }
        #endregion

        #region Region 2 Test Method GetPromotionByDate

         [Fact]
        public async Task GetPromotionByDateTest() // positive
        {
            //Arrange
            var promotionID = Guid.NewGuid();
            var list= new List<Promotion>();
            var promotion1 = new Promotion
            {
                Id = promotionID,
                PaymentMethods = new string[] { "TARJETA_CREDITO", "TARJETA_DEBITO", "EFECTIVO", "GIFT_CARD" },
                Banks = new string[] { "SANTANDER RIO", "ICBC" },
                ProductCategories = new string[] { "Hogar", "Jardin", "ElectroCocina", "GrandesElectro", "Colchones", "Celulares", "Tecnologia", "Audio" },
                MaximumAmountInstallments = 12,
                InterestValueFees = 0,
                DiscountPercentage = null,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(60),
                Active=true,
                CreationDate= DateTime.Now
            };

            var promotion2 = new Promotion
            {
                Id = promotionID,
                PaymentMethods = new string[] { "EFECTIVO", "GIFT_CARD" },
                Banks = new string[] { },
                ProductCategories = new string[] { "Colchones", "Celulares", "Tecnologia", "Audio" },
                MaximumAmountInstallments = 0,
                InterestValueFees = 0,
                DiscountPercentage = 10,
                StartDate = DateTime.Now.AddDays(4).Date,
                EndDate = DateTime.Now.AddDays(60).Date,
                Active = true,
                CreationDate = DateTime.Now
            };

            list.Add(promotion1);
            list.Add(promotion2);
            var fecha = DateTime.Now.AddDays(5);
             _promotrionRepositoryMock.Setup(x => x.GetCurrentPromotionByDate(fecha)).
                ReturnsAsync(list);

            //Act
            List<Promotion> promotions = await _promServ.GetCurrentPromotionByDate(fecha);
            //Asert
             Assert.Equal(2,Convert.ToInt32(promotions.Count));
        }


        [Fact]
        public async Task GetPromotionByDateNotExistTest() // negative
        {
            //Arrange
            var fecha = DateTime.Now;

            _promotrionRepositoryMock.Setup(x => x.GetCurrentPromotionByDate(It.IsAny<DateTime>())).
                ReturnsAsync(() => null);

            //Act
            List<Promotion> promotion = await _promServ.GetCurrentPromotionByDate(fecha);
            //Asert
            Assert.Null(promotion);
       
        }
        #endregion

        #region Region 3 Test Method GetAllPromotion

          [Fact]
        public async Task GetAllPromotionTest() // positive
        {
            //Arrange
            var promotionID = Guid.NewGuid();
            var list = new List<Promotion>();
            var promotion1 = new Promotion
            {
                Id = promotionID,
                PaymentMethods = new string[] { "TARJETA_CREDITO", "TARJETA_DEBITO", "EFECTIVO", "GIFT_CARD" },
                Banks = new string[] { "SANTANDER RIO", "ICBC" },
                ProductCategories = new string[] { "Hogar", "Jardin", "ElectroCocina", "GrandesElectro", "Colchones", "Celulares", "Tecnologia", "Audio" },
                MaximumAmountInstallments = 12,
                InterestValueFees = 0,
                DiscountPercentage = null,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(60),
                Active = true,
                CreationDate = DateTime.Now
            };

            var promotion2 = new Promotion
            {
                Id = promotionID,
                PaymentMethods = new string[] { "EFECTIVO", "GIFT_CARD" },
                Banks = new string[] { },
                ProductCategories = new string[] { "Colchones", "Celulares", "Tecnologia", "Audio" },
                MaximumAmountInstallments = 0,
                InterestValueFees = 0,
                DiscountPercentage = 10,
                StartDate = DateTime.Now.AddDays(4),
                EndDate = DateTime.Now.AddDays(60),
                Active = true,
                CreationDate = DateTime.Now
            };

            list.Add(promotion1);
            list.Add(promotion2);

            _promotrionRepositoryMock.Setup(x => x.GetAllPromotion()).
               ReturnsAsync(list);

            //Act
            List<Promotion> promotions = await _promServ.GetAllPromotion();
            //Asert
            Assert.Equal(2, Convert.ToInt32(promotions.Count));
        }


        [Fact]
        public async Task GetAllPromotionNotExistTest() // negative
        {
            //Arrange
         
            _promotrionRepositoryMock.Setup(x => x.GetAllPromotion()).
                ReturnsAsync(() => null);

            //Act
            List<Promotion> promotion = await _promServ.GetAllPromotion();
            //Asert
            Assert.Null(promotion);

        }
        #endregion

        #region Region 4 Test Method GetCurrentPromotion

        [Fact]
        public async Task GetCurrentPromotionTest() // positive
        {
            //Arrange
            var promotionID = Guid.NewGuid();
            var list = new List<Promotion>();
            var promotion1 = new Promotion
            {
                Id = promotionID,
                PaymentMethods = new string[] { "TARJETA_CREDITO", "TARJETA_DEBITO", "EFECTIVO", "GIFT_CARD" },
                Banks = new string[] { "SANTANDER RIO", "ICBC" },
                ProductCategories = new string[] { "Hogar", "Jardin", "ElectroCocina", "GrandesElectro", "Colchones", "Celulares", "Tecnologia", "Audio" },
                MaximumAmountInstallments = 12,
                InterestValueFees = 0,
                DiscountPercentage = null,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(60),
                Active = true,
                CreationDate = DateTime.Now
            };

            var promotion2 = new Promotion
            {
                Id = promotionID,
                PaymentMethods = new string[] { "EFECTIVO", "GIFT_CARD" },
                Banks = new string[] { },
                ProductCategories = new string[] { "Colchones", "Celulares", "Tecnologia", "Audio" },
                MaximumAmountInstallments = 0,
                InterestValueFees = 0,
                DiscountPercentage = 10,
                StartDate = DateTime.Now.AddDays(4),
                EndDate = DateTime.Now.AddDays(60),
                Active = true,
                CreationDate = DateTime.Now
            };

            list.Add(promotion1);
            list.Add(promotion2);

            _promotrionRepositoryMock.Setup(x => x.GetCurrentPromotion()).
               ReturnsAsync(list);

            //Act
            List<Promotion> promotions = await _promServ.GetCurrentPromotion();
            //Asert
            Assert.Equal(2, Convert.ToInt32(promotions.Count));
        }


        [Fact]
        public async Task GetCurrentPromotionNotExistTest() // negative
        {
            //Arrange

            _promotrionRepositoryMock.Setup(x => x.GetCurrentPromotion()).
                ReturnsAsync(() => null);

            //Act
            List<Promotion> promotion = await _promServ.GetCurrentPromotion();
            //Asert
            Assert.Null(promotion);

        }
        #endregion

        #region Region 5 Test Method GetCurrentPromotionByDateSale
         [Fact]
        public async Task GetCurrentPromotionByDateSaleTest() // positive
        {
            //Arrange
            var promotionID = Guid.NewGuid();
            var list = new List<Promotion>();
            var promotion1 = new Promotion
            {
                Id = promotionID,
                PaymentMethods = new string[] { "TARJETA_CREDITO", "TARJETA_DEBITO", "EFECTIVO", "GIFT_CARD" },
                Banks = new string[] { "SANTANDER RIO", "ICBC" },
                ProductCategories = new string[] { "Hogar", "Jardin", "ElectroCocina", "GrandesElectro", "Colchones", "Celulares", "Tecnologia", "Audio" },
                MaximumAmountInstallments = 12,
                InterestValueFees = 0,
                DiscountPercentage = null,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(60),
                Active = true,
                CreationDate = DateTime.Now
            };

            var promotion2 = new Promotion
            {
                Id = promotionID,
                PaymentMethods = new string[] { "TARJETA_CREDITO","EFECTIVO", "GIFT_CARD" },
                Banks = new string[] {"SANTANDER RIO" },
                ProductCategories = new string[] { "Colchones", "Celulares", "Tecnologia", "Audio" },
                MaximumAmountInstallments = 0,
                InterestValueFees = 0,
                DiscountPercentage = 10,
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.AddDays(60).Date,
                Active = true,
                CreationDate = DateTime.Now
            };

            list.Add(promotion1);
            list.Add(promotion2);
            _promotrionRepositoryMock.Setup(x => x.GetCurrentPromotionBySale("TARJETA_CREDITO", "SANTANDER RIO", new string[] { "Celulares", "Tecnologia" })).
               ReturnsAsync(list);

            //Act
            List<Promotion> promotions = await _promServ.GetCurrentPromotionBySale("TARJETA_CREDITO", "SANTANDER RIO", new string[] { "Celulares", "Tecnologia" });
            //Asert
            Assert.Equal(2, Convert.ToInt32(promotions.Count));
        }


        [Fact]
        public async Task GetCurrentPromotionByDateSaleNotExistTest() // negative
        {
            //Arrange
          
            _promotrionRepositoryMock.Setup(x => x.GetCurrentPromotionBySale(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IEnumerable<string>>())).
                ReturnsAsync(() => null);

            //Act
            List<Promotion> promotion = await _promServ.GetCurrentPromotionBySale(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IEnumerable<string>>());
            //Asert
            Assert.Null(promotion);

        }
        #endregion

        #region Region 6 Test Method CreatePromotion
        [Fact]
        public async Task CreatePromotionTest() // positive
        {
            //Arrange
            var promotionID = Guid.NewGuid();
            var promotion1 = new Promotion
            {
                Id = promotionID,
                PaymentMethods = new string[] { "TARJETA_CREDITO", "TARJETA_DEBITO", "EFECTIVO", "GIFT_CARD" },
                Banks = new string[] { "SANTANDER RIO", "ICBC" },
                ProductCategories = new string[] { "Hogar", "Jardin", "ElectroCocina", "GrandesElectro", "Colchones", "Celulares", "Tecnologia", "Audio" },
                MaximumAmountInstallments = 12,
                InterestValueFees = 0,
                DiscountPercentage = null,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(60),
    
            };
            _promotrionRepositoryMock.Setup(x => x.InsertPromotion(promotion1));

            //Act
            await _promServ.InsertPromotion(promotion1);

            //Asert
            Assert.NotNull( _promServ.InsertPromotion(promotion1) as object);
        }

        #endregion

        #region Region 7 Test Method UpdatePromotion
        [Fact]
        public async Task UpdatePromotionTest() // positive
        {
            //Arrange
            var promotionID = Guid.NewGuid();
            var promotion1 = new Promotion
            {
                Id = promotionID,
                PaymentMethods = new string[] { "TARJETA_CREDITO", "TARJETA_DEBITO", "EFECTIVO", "GIFT_CARD" },
                Banks = new string[] { "SANTANDER RIO", "ICBC" },
                ProductCategories = new string[] { "Hogar", "Jardin", "ElectroCocina", "GrandesElectro", "Colchones", "Celulares", "Tecnologia", "Audio" },
                MaximumAmountInstallments = 12,
                InterestValueFees = 0,
                DiscountPercentage = null,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(60),

            };
            _promotrionRepositoryMock.Setup(x => x.UpdatePromotion(promotion1));

            //Act
            await _promServ.UpdatePromotion(promotion1);

            //Asert
            Assert.NotNull(_promServ.UpdatePromotion(promotion1) as object);
        }

        #endregion

    }
}
