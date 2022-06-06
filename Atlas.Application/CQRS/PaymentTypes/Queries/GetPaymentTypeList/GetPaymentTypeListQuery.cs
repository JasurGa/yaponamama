using System;
using MediatR;

namespace Atlas.Application.CQRS.PaymentTypes.Queries.GetPaymentTypeList
{
    public class GetPaymentTypeListQuery : IRequest<PaymentTypeListVm>
    {
    }
}
