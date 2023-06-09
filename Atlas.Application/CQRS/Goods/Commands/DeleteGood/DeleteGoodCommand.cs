﻿using System;
using MediatR;

namespace Atlas.Application.CQRS.Goods.Commands.DeleteGood
{
    public class DeleteGoodCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
