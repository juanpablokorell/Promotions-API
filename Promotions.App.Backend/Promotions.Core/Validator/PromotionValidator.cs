using FluentValidation;
using Promotions.Core.DTOs;
using Promotions.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Promotions.Core.Validator
{
    public class PromotionValidator: AbstractValidator<PromotionDTO>
    {
        public PromotionValidator()
        {
            
            RuleFor(promotion => promotion.StartDate)
                .NotNull()
                .WithMessage("Fecha Inicio es Requerida");

           
            RuleFor(promotion => promotion.EndDate)
                .NotNull()
                .WithMessage("Fecha Fin es Requerida");

            RuleFor(promotion => promotion.EndDate)
                          .GreaterThan(promotion=>promotion.StartDate)
                          .WithMessage("Fecha Fin debe ser Mayor a Fecha Inicio");



            RuleFor(promotion => promotion.MaximumAmountInstallments)
                .NotNull()
                .When(promotion => promotion.DiscountPercentage is null)
                .WithMessage("Al menos cantidad de cuotas o procentaje de descuento es Requerido");

            RuleFor(promotion => promotion.MaximumAmountInstallments)
             .Null()
             .When(promotion => promotion.DiscountPercentage>0 )
             .WithMessage("Se permite solo una promoocion. Porcentaje de descuento o Cuotas ");


            RuleFor(promotion => promotion.InterestValueFees)
                .NotNull()
                .When(promotion => promotion.MaximumAmountInstallments>0)
                .WithMessage("Complete Porcentaje Interes");


            RuleFor(promotion => promotion.DiscountPercentage)
           .InclusiveBetween(5,80)
           .When(promotion => promotion.DiscountPercentage > 0)
           .WithMessage("Rango permitido entre 5 y 80");


    
        }
    }
}
