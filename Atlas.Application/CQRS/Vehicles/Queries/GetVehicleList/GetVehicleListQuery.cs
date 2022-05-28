using MediatR;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehicleList
{
    public class GetVehicleListQuery : IRequest<VehicleListVm>
    {
    }
}
