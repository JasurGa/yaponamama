using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;

namespace Atlas.Application.CQRS.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateUserCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        private static string GenerateSalt()
        {
            Random random = new();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetHash(string s)
        {
            var sb = new StringBuilder();

            using (var hash = SHA256Managed.Create())
            {
                var enc = Encoding.UTF8;
                var result = hash.ComputeHash(enc.GetBytes(s));

                foreach (var b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }

            return sb.ToString().ToUpper();
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var salt = GenerateSalt();

            var user = new User
            {
                Id              = Guid.NewGuid(),
                Login           = request.Login,
                Salt            = salt,
                PasswordHash    = GetHash(salt + request.Password),
                FirstName       = request.FirstName,
                LastName        = request.LastName,
                Birthday        = request.Birthday,
                AvatarPhotoPath = request.AvatarPhotoPath,
                IsDeleted       = false,
                CreatedAt       = DateTime.UtcNow,
            };

            await _dbContext.Users.AddAsync(user,
                cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
