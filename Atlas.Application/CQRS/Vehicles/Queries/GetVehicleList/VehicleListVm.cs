using System.Collections.Generic;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehicleList
{
    public class VehicleListVm
    {
        public IList<VehicleLookupDto> Vehicles { get; set; }
    }
}
