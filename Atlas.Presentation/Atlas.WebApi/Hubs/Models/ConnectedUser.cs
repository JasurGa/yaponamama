using System;
namespace Atlas.WebApi.Hubs.Models
{
    public class ConnectedUser
    {
        public string ConnectionId { get; set; }

        public Guid UserId { get; set; }
    }
}
