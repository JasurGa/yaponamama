﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Providers.Commands.CreateProvider
{
    public class CreateProviderCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public float Longitude { get; set; }

        public float Latitude { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string LogotypePath { get; set; }
    }
}
