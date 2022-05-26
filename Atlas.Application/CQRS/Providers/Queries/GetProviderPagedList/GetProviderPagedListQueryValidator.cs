using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Atlas.Application.CQRS.Providers.Queries.GetProviderPagedList
{
    public class GetProviderPagedListQueryValidator : AbstractValidator<GetProviderPagedListQuery>
    {
        public GetProviderPagedListQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
