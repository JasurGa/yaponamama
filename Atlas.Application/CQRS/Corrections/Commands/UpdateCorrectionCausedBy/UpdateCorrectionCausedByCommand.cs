using MediatR;
using System;

namespace Atlas.Application.CQRS.Corrections.Commands.UpdateCorrectionCausedBy
{
    public class UpdateCorrectionCausedByCommand : IRequest
    {
        public Guid Id { get; set; }

        public string CausedBy { get; set; }
    }
}
