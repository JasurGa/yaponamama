using MediatR;
using System;

namespace Atlas.Application.CQRS.Corrections.Commands.CreateCorrection
{
    public class CreateCorrectionCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }

        public Guid StoreId { get; set; }

        public Guid GoodId { get; set; }

        public string CauseBy { get; set; }

        public int Count { get; set; }
    }
}
