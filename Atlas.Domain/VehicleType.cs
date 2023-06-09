﻿using System;
using System.Collections.Generic;

namespace Atlas.Domain
{
    public class VehicleType
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
