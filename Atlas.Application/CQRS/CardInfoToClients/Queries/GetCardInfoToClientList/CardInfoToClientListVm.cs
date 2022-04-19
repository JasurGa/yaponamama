using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.CardInfoToClients.Queries.GetCardInfoToClientList
{
    public class CardInfoToClientListVm
    {
        public List<CardInfoToClientLookupDto> CardInfoToClients { get; set; }
    }
}
