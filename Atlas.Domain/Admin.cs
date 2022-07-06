using System;
namespace Atlas.Domain
{
    public class Admin
    {
        public Guid Id { get; set; }

        public DateTime StartOfWorkingHours { get; set; }

        public long WorkingDayDuration { get; set; }

        public long KPI { get; set; }

        public Guid OfficialRoleId { get; set; }

        public OfficialRole OfficialRole { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public bool IsDeleted { get; set; }
    }
}
