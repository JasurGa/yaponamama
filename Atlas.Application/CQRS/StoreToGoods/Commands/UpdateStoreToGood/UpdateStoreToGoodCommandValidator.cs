using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Atlas.Application.CQRS.StoreToGoods.Commands.UpdateStoreToGood
{
    public class UpdateStoreToGoodCommandValidator : AbstractValidator<UpdateStoreToGoodCommand>
    {
        public UpdateStoreToGoodCommandValidator()
        {
            RuleFor(stg => stg.Id)
                .NotEqual(Guid.Empty);

            RuleFor(stg => stg.Count)
                .NotEmpty();
        }
    }
}
