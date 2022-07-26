using System;
using MediatR;

namespace Atlas.Application.CQRS.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public int Sex { get; set; }

        public DateTime Birthday { get; set; }
        
        public string AvatarPhotoPath { get; set; }
    }
}
