using System;

namespace Atlas.Application.CQRS.VerificationRequests.Queries.GetMyVerificationRequests
{
    public class VerificationRequestClientDetailsVm
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime Birthday { get; set; }

        public string PhoneNumber { get; set; }
    }
}
