﻿using System;
namespace Atlas.Identity.Models
{
    public class ChangePasswordDto
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
