﻿using System;
namespace Atlas.Domain
{
    public class User
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Salt { get; set; }

        public string PasswordHash { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? Birthday { get; set; }

        public string AvatarPhotoPath { get; set; }

        public bool IsDeleted { get; set; }
    }
}
