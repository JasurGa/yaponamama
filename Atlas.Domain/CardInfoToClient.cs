using System;
namespace Atlas.Domain
{
    public class CardInfoToClient
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public string Name { get; set; }

        public string CardNumber { get; set; }

        public string DateOfIssue { get; set; }

        public string Cvc { get; set; }

        public string Cvc2 { get;set; }

        public string CardHolder { get; set; }

        public Client Client { get; set; }
    }
}
