﻿using Atlas.Application.CQRS.Clients.Queries.GetClientsList;
using Atlas.Application.Models;
using MediatR;
using System;

namespace Atlas.Application.CQRS.Clients.Queries.GetClientPagedList
{
    public class GetClientPagedListQuery : IRequest<PageDto<ClientLookupDto>>
    {
        public bool ShowDeleted { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public string Sortable { get; set; }

        public bool Ascending { get; set; }

        public DateTime? FilterFromCreatedAt { get; set; }

        public DateTime? FilterToCreatedAt { get; set; }
    }
}
