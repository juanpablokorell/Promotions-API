
using System;
using System.Collections.Generic;
using System.Text;

namespace Promotions.Core.Entities
{
    public class Promotion
    {
        public Guid Id { get; set; }

        public IEnumerable<string> PaymentMethods { get; set; }
           
        public IEnumerable<string> Banks { get;  set; }
        public IEnumerable<string> ProductCategories { get;  set; }
        public int? MaximumAmountInstallments { get;  set; }
        public decimal? InterestValueFees { get;  set; }
        public decimal? DiscountPercentage { get;  set; }
        public DateTime? StartDate { get;  set; }
        public DateTime? EndDate { get;  set; }


        public Boolean Active { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

    }
}
