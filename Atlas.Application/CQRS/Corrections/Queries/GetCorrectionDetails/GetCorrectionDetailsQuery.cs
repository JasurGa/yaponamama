using MediatR;
using System;

namespace Atlas.Application.CQRS.Corrections.Queries.GetCorrectionDetails
{
    public class GetCorrectionDetailsQuery : IRequest<CorrectionDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
