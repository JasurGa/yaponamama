using System;
namespace Atlas.Domain
{
    public class HeadRecruiter
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string PassportPhotoPath { get; set; }

        public bool IsDeleted { get; set; }

        public User User { get; set; }
    }
}
