using System.Collections.Generic;

namespace Atlas.Application.CQRS.PaymentTypes.Queries.GetPaymentTypeList
{
    public class PaymentTypeListVm
    {
        public IList<PaymentTypeLookupDto> PaymentTypes { get; set; }
    }
}