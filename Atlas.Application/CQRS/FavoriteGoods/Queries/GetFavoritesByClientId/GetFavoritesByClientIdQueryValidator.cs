using System;
using FluentValidation;

namespace Atlas.Application.CQRS.FavoriteGoods.Queries.GetFavoritesByClientId
{
    public class GetFavoritesByClientIdQueryValidator :
        AbstractValidator<GetFavoritesByClientIdQuery>
    {
        public GetFavoritesByClientIdQueryValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}
