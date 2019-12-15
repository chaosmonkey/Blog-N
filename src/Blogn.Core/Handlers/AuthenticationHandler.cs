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
    public class AuthenticationHandler: IRequestHandler<Authenticate, AuthenticationResult>
    {
        public AuthenticationHandler(ILogger<AuthenticationHandler> logger, IAccountRepository repository, IPasswordHasher hasher)
        {
            Logger = Guard.IsNotNull(logger, nameof(logger));
            Repository = Guard.IsNotNull(repository, nameof(repository));
            Hasher = Guard.IsNotNull(hasher, nameof(hasher));
        }

        protected IPasswordHasher Hasher { get; }
        protected ILogger Logger { get; }
        protected IAccountRepository Repository { get; }

        public async Task<AuthenticationResult> Handle(Authenticate request, CancellationToken cancellationToken)
        {
            try
            {
                Logger.LogInformation($"Beginning Authentication for '{request.Email}'.");
                var account = await Repository.RetrieveAccountAsync(request.Email.Trim());
                if (account == null)
                {
                    Logger.LogWarning($"User '{request.Email.Trim()}' was not found.");
                    return AuthenticationResult.Failed();
                }
                var passwordCheck = Hasher.VerifyHashedPassword(account.Credentials.Password, request.Password);
                if (passwordCheck == PasswordVerificationResult.Failed)
                {
                    Logger.LogWarning($"Authentication failed password validation for user '{request.Email}'.");
                    return AuthenticationResult.Failed();
                }
                if (passwordCheck == PasswordVerificationResult.SuccessRehashNeeded)
                {
                    Logger.LogInformation("Password rehash required.");
                    var credentials = account.Credentials;
                    credentials.Password = Hasher.HashPassword(request.Password);
                    Logger.LogDebug("Updating credentials.");
                    Repository.UpdateCredentials(credentials);
                    await Repository.SaveAsync();
                    Logger.LogInformation("Credentials Updated.");
                }
                if (!account.IsEnabled)
                {
                    return AuthenticationResult.Failed(true);
                }
                account.Credentials = null;
                Logger.LogInformation("Authentication successful.");
                return AuthenticationResult.Success(account);
            }
            catch (Exception ex)
            {
                Logger.LogException($"Exception while attempting to authenticate {request.Email}.", ex);
                return AuthenticationResult.ServerError("An error occured on the server during authentication.");
            }
        }
    }
}
