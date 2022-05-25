using FluentValidation;

namespace Atlas.Application.CQRS.Consignments.Queries.GetConsignmentList
{
    public class GetConsignmentListQueryValidator : AbstractValidator<GetConsignmentListQuery>
    {
        public GetConsignmentListQueryValidator()
        {

        }
    }
}
