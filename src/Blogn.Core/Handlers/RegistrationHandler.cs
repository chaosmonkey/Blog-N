using System;
using System.Threading;
using System.Threading.Tasks;
using Blogn.Commands;
using Blogn.Data;
using Blogn.Models;
using Blogn.Responses;
using Blogn.Services;
using ChaosMonkey.Guards;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blogn.Handlers
{
    public class RegistrationHandler: IRequestHandler<Register, RegistrationResult>
    {
        public RegistrationHandler(ILogger<RegistrationHandler> logger, IAccountRepository repository, 
            IAvatarService avatars, IPasswordHasher hasher, ITimeProvider time)
        {
            Logger = Guard.IsNotNull(logger, nameof(logger));
            Repository = Guard.IsNotNull(repository, nameof(repository));
            Avatars = Guard.IsNotNull(avatars, nameof(avatars));
            Hasher = Guard.IsNotNull(hasher, nameof(hasher));
            Time = Guard.IsNotNull(time, nameof(time));
        }

        protected ILogger Logger { get; }
        protected IAccountRepository Repository { get; }
        protected IAvatarService Avatars { get; }
        protected IPasswordHasher Hasher { get; }
        protected ITimeProvider Time { get; }

        public async Task<RegistrationResult> Handle(Register request, CancellationToken cancellationToken)
        {
            try
            {
                if (await Repository.CheckIfAccountExistsAsync(request.Email))
                {
                    return RegistrationResult.AccountExists(request.Email);
                }

                var now = Time.NowUtc;
                var account = new Account
                {
                    Email = request.Email.Trim(),
                    DateUpdated = now,
                    IsEnabled = true,
                    DateCreated = now,
                    AvatarId = Avatars.CalculateAvatarId(request.Email),
                    Credentials = new Credentials
                    {
                        DateUpdated = now,
                        DateCreated = now,
                        Password = Hasher.HashPassword(request.Password.Trim())
                    },
                    DisplayName = request.DisplayName.Trim(),
                    Roles = DefaultRoles.DefaultAccountRoles
                };
                Repository.AddAccount(account);
                await Repository.SaveAsync();
                return RegistrationResult.Success(account);
            }
            catch (Exception ex)
            {
                Logger.LogException($"Exception encountered registering account {request.Email}.", ex);
                return RegistrationResult.ServerError("An error occured on the server during registration.");
            }
        }
    }
}
