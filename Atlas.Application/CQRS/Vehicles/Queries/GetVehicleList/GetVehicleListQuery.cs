﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehicleList
{
    public class GetVehicleListQuery : IRequest<VehicleListVm>
    {

    }
}