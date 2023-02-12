using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.PromoAdvertises.Queries.GetPromoAdvertiseById
{
    public class GetPromoAdvertiseByIdQuery : IRequest<PromoAdvertiseDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
