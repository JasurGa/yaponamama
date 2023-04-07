using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Clients.Commands.VerifyClient
{
    public class VerifyClientCommand : IRequest
    {
        public Guid ClientId { get; set; }
        
        public bool IsPassportVerified { get; set; }
    }
}
