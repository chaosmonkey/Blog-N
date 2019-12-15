using System;
using System.Threading;
using System.Threading.Tasks;
using Blogn.Commands;
using Blogn.Data;
using Blogn.Responses;
using Blogn.Services;
using ChaosMonkey.Guards;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Blogn.Handlers
{
    public class ChangePasswordHandler: IRequestHandler<ChangePassword, ChangePasswordResponse>
    {
        public ChangePasswordHandler(ILogger<ChangePasswordHandler> logger, IAccountRepository repository,
            IPasswordHasher hasher, ITimeProvider time)
        {
            Logger = Guard.IsNotNull(logger, nameof(logger));
            Repository = Guard.IsNotNull(repository, nameof(repository));
            Hasher = Guard.IsNotNull(hasher, nameof(hasher));
            Time = Guard.IsNotNull(time, nameof(time));
        }

        protected ILogger Logger { get; }
        protected IAccountRepository Repository { get; }
        protected IPasswordHasher Hasher { get; }
        protected ITimeProvider Time { get; }

        public async Task<ChangePasswordResponse> Handle(ChangePassword request, CancellationToken cancellationToken)
        {
            try
            {
                var credentials = await Repository.RetrieveCredentialsAsync(request.AccountId);
                if (credentials == null)
                {
                    Logger.LogWarning($"While attempting to update password, no credentials were found for account '{request.AccountId}'.");
                    return ChangePasswordResponse.NotFound(
                        $"An error occurred while updating your password.  A credential record was not found.  Please contact the administrator for assistance.");
                }

                var verification = Hasher.VerifyHashedPassword(credentials.Password, request.CurrentPassword);
                if (verification != PasswordVerificationResult.Failed)
                {
                    credentials.DateUpdated = Time.NowUtc;
                    credentials.Password = Hasher.HashPassword(request.NewPassword);
                    await Repository.SaveAsync();
                    return ChangePasswordResponse.Success();
                }

                Logger.LogWarning($"An attempt to change the password for account '{request.AccountId}' failed because the current password was incorrect.");
                return ChangePasswordResponse.Error("Current password verification failed.");
            }
            catch (Exception ex)
            {
                Logger.LogException("", ex);
                return ChangePasswordResponse.ServerError("An error occurred while updating your password.");
            }

        }
    }
}
