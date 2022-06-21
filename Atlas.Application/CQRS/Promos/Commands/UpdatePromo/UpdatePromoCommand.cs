using MediatR;
using System;

namespace Atlas.Application.CQRS.Promos.Commands.UpdatePromo
{
    public class UpdatePromoCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid GoodId { get; set; }

        public string Name { get; set; }

        public int DiscountPrice { get; set; }

        public int DiscountPercent { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}
