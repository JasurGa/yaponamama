using FluentValidation;

namespace Atlas.Application.CQRS.Providers.Queries.GetProviderList
{
    public class GetProviderListQueryValidator : AbstractValidator<GetProviderListQuery>
    {
        public GetProviderListQueryValidator()
        {

        }
    }
}
