using System.Collections.Generic;

namespace Atlas.Application.CQRS.VehicleTypes.Queries.GetVehicleTypeList
{
    public class VehicleTypeListVm
    {
        public IList<VehicleTypeLookupDto> VehicleTypes { get; set; }
    }
}