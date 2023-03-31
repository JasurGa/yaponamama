using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.GoodToCarts.Commands.DeleteAllGoodToCarts
{
    public class DeleteAllGoodToCartsCommandValidator : 
        AbstractValidator<DeleteAllGoodToCartsCommand>
    {
        public DeleteAllGoodToCartsCommandValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}
