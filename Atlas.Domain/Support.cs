using System;
namespace Atlas.Domain
{
    public class Support
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime StartOfWorkingHours { get; set; }

        public long WorkingDayDuration { get; set; }

        public int Salary { get; set; }

        public long KPI { get; set; }

        public string InternalPhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }

        public bool IsDeleted { get; set; }

        public User User { get; set; }
    }
}
