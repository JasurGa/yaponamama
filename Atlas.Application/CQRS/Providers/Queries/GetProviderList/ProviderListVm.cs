﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Providers.Queries.GetProviderList
{
    public class ProviderListVm
    {
        public IList<ProviderLookupDto> Providers { get; set; }
    }
}