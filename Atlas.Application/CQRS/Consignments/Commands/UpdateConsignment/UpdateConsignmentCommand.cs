﻿using MediatR;
using System;

namespace Atlas.Application.CQRS.Consignments.Commands.UpdateConsignment
{
    public class UpdateConsignmentCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid StoreToGoodId { get; set; }

        public DateTime PurchasedAt { get; set; }

        public DateTime ExpirateAt { get; set; }

        public string ShelfLocation { get; set; }
    }
}