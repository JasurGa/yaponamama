using System;
using MediatR;

namespace Atlas.Application.CQRS.Statistics.Queries.GetDebitCreditStaistics
{
    public class GetDebitCreditStatisticsQuery : IRequest<DebitCreditLookupDto>
    {
    }
}

