using MediatR;
using System;

namespace Atlas.Application.CQRS.Promos.Commands.CreatePromo
{
    public class CreatePromoCommand : IRequest<Guid>
    {
        public Guid GoodId { get; set; }

        public string Name { get; set; }

        public int DiscountPrice { get; set; }

        public int DiscountPercent { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}
