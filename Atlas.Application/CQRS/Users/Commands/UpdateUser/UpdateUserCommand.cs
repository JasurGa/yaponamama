﻿using System;
using MediatR;

namespace Atlas.Application.CQRS.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public DateTime Birthday { get; set; }
        
        public string AvatarPhotoPath { get; set; }
    }
}
