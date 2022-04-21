using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Couriers.Queries.GetCourierDetails
{
    public class GetCourierDetailsQuery : IRequest<CourierDetailsVm>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string PhoneNumber { get; set; }

        public long Balance { get; set; }

        public long KPI { get; set; }

        public Guid VehicleId { get; set; }

    }
}
