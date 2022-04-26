using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
