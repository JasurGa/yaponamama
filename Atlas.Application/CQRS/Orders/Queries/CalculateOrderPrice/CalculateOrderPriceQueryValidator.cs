using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Orders.Queries.CalculateOrderPrice
{
    public class CalculateOrderPriceQueryValidator : AbstractValidator<CalculateOrderPriceQuery>
    {
        public CalculateOrderPriceQueryValidator() 
        { 
        }
    }
}
