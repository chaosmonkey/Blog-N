using System;
using System.Collections.Generic;
using System.Text;
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
    public class ValidateResetTokenHandler: IRequestHandler<ValidateResetToken, ValidateResetTokenResponse>
    {
        public ValidateResetTokenHandler(ILogger<ValidateResetTokenHandler> logger, IAccountRepository repository, ITimeProvider time)
        {
            Logger = Guard.IsNotNull(logger, nameof(logger));
            Repository = Guard.IsNotNull(repository, nameof(repository));
            Time = Guard.IsNotNull(time, nameof(time));
        }

        protected ILogger Logger { get; }
        protected IAccountRepository Repository { get; }
        protected ITimeProvider Time { get; }

        public async Task<ValidateResetTokenResponse> Handle(ValidateResetToken request, CancellationToken cancellationToken)
        {
            try
            {
                var now = Time.NowUtc;
                var token = await Repository.RetrieveResetToken(request.Token);
                if (token == null)
                {
                    Logger.LogInformation($"Password reset token '{request.Token} was not found.");
                    return ValidateResetTokenResponse.NotFound();
                }
                if (token.DateExpired < now)
                {
                    Logger.LogInformation($"Password reset token '{request.Token} was found, but was expired {token.DateExpired}.");
                    return ValidateResetTokenResponse.NotFound();
                }
                if (token.DateConsumed.HasValue)
                {
                    Logger.LogInformation($"Password reset token '{request.Token} was found, but was already consumed.");
                    return ValidateResetTokenResponse.NotFound();
                }
                Logger.LogDebug($"Password reset token '{request.Token}' is valid. (It exists, is not expired, and is not consumed.)");
                return ValidateResetTokenResponse.Success();
            }
            catch(Exception ex)
            {
                Logger.LogException($"Exception occurred while validating reset token '{request.Token}'.", ex);
                return ValidateResetTokenResponse.Error("An error occurred on the server while processing yor request.");
            }

        }
    }
}
