using System;
using MediatR;

namespace Atlas.Application.CQRS.PromoAdvertises.Commands.UpdatePromoAdvertise
{
    public class UpdatePromoAdvertiseCommand : IRequest
    {
        public Guid Id { get; set; }

        public string WideBackground { get; set; }

        public string HighBackground { get; set; }

        public string TitleColor { get; set; }

        public string TitleRu { get; set; }

        public string TitleEn { get; set; }

        public string TitleUz { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}

