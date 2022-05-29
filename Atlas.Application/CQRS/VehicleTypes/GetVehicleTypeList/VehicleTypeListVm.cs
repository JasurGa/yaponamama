using System.Collections.Generic;

namespace Atlas.Application.CQRS.VehicleTypes.GetVehicleTypeList
{
    public class VehicleTypeListVm
    {
        public IList<VehicleTypeLookupDto> VehicleTypes { get; set; }
    }
}