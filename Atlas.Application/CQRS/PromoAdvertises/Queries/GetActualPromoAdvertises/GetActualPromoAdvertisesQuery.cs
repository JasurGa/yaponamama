using System;
using MediatR;

namespace Atlas.Application.CQRS.PromoAdvertises.Queries.GetActualPromoAdvertises
{
    public class GetActualPromoAdvertisesQuery : IRequest<PromoAdvertisesListVm>
    {
    }
}

