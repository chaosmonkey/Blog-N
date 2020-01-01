using System;
using System.Threading;
using System.Threading.Tasks;
using Blogn.Commands;
using Blogn.Data;
using Blogn.Responses;
using Blogn.Services;
using ChaosMonkey.Guards;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blogn.Handlers
{
    public class ResetPasswordHandler : IRequestHandler<ResetPassword, ResetPasswordResponse>
    {
        public ResetPasswordHandler(ILogger<ResetPasswordHandler> logger, IAccountRepository repository, 
                                    IPasswordHasher hasher, ITimeProvider time)
        {
            Logger = Guard.IsNotNull(logger, nameof(logger));
            Repository = Guard.IsNotNull(repository, nameof(repository));
            Time = Guard.IsNotNull(time, nameof(time));
            Hasher = Guard.IsNotNull(hasher, nameof(hasher));
        }

        protected ILogger Logger { get; }
        protected IAccountRepository Repository { get; }
        protected ITimeProvider Time { get; }
        protected IPasswordHasher Hasher { get; }

        public async Task<ResetPasswordResponse> Handle(ResetPassword request, CancellationToken cancellationToken)
        {
            try
            {
                var now = Time.NowUtc;
                var token = await Repository.RetrieveResetToken(request.Token);
                // So as to avoid disclosing any information to a would be attacker, we will return not found for all cases 
                // where the token is not found or is not valid for some reason. (already consumed, email doesn't match, etc.)
                // Most of these cases should never happen since we will be validating the token before displaying the form, but JIC...
                if(token == null)
                {
                    Logger.LogInformation($"Password reset token '{request.Token} was not found.");
                    return ResetPasswordResponse.NotFound();
                }
                if (token.DateExpired < now)
                {
                    Logger.LogInformation($"Password reset token '{request.Token} was found, but was expired {token.DateExpired}.");
                    return ResetPasswordResponse.NotFound();
                }
                if (token.DateConsumed.HasValue)
                {
                    Logger.LogInformation($"Password reset token '{request.Token} was found, but was already consumed.");
                    return ResetPasswordResponse.NotFound();
                }
                if (!string.Equals(token.Credentials.Account.Email, request.Email, StringComparison.CurrentCultureIgnoreCase))
                {
                    Logger.LogInformation($"Password reset token '{request.Token} was found, but email did not match.");
                    return ResetPasswordResponse.NotFound();
                }
                if(token!=null 
                    && token.DateExpired < now 
                    && !token.DateConsumed.HasValue 
                    && string.Equals(token.Credentials.Account.Email, request.Email, StringComparison.CurrentCultureIgnoreCase))
                {
                    token.Credentials.Password = Hasher.HashPassword(request.Password);
                    token.Consume(now);
                    await Repository.SaveAsync();
                    Logger.LogInformation($"Password for '{request.Email}' was updated using token '{request.Token}'.");
                    return ResetPasswordResponse.Success();
                }
                Logger.LogWarning("An unexpected condition has occurred during password reset and an error will be returned.");
                return ResetPasswordResponse.Error("An unexpected error has occurred on the server and the password was unable to be reset.");
            }
            catch(Exception ex)
            {
                Logger.LogException("Error processing password reset", ex);
                return ResetPasswordResponse.Error("An unexpected error has occurred on the server.");
            }
            
        }
    }
}
