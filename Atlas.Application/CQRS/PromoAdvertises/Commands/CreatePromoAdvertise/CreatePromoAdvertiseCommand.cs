using System;
using MediatR;

namespace Atlas.Application.CQRS.PromoAdvertises.Commands.CreatePromoAdvertise
{
    public class CreatePromoAdvertiseCommand : IRequest<Guid>
    {
        public string WideBackground { get; set; }

        public string HighBackground { get; set; }

        public string TitleColor { get; set; }

        public string TitleRu { get; set; }

        public string TitleEn { get; set; }

        public string TitleUz { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}

