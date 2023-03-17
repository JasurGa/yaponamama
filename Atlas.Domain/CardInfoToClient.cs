using System;
namespace Atlas.Domain
{
    public class CardInfoToClient
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public string Expire { get; set; }

        public string Token { get; set; }

        public bool Recurrent { get; set; }

        public bool Verify { get; set; }

        public Client Client { get; set; }
    }
}
