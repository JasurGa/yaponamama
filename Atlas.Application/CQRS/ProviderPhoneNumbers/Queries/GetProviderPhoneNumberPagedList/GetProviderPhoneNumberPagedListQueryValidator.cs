using FluentValidation;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Queries.GetProviderPhoneNumberPagedList
{
    public class GetProviderPhoneNumberPagedListQueryValidator :
        AbstractValidator<GetProviderPhoneNumberPagedListQuery>
    {
        public GetProviderPhoneNumberPagedListQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
