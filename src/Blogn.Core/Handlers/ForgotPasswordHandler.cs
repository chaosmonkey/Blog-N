using System;
using System.Threading;
using System.Threading.Tasks;
using Blogn.Commands;
using Blogn.Configuration;
using Blogn.Data;
using Blogn.Infrastructure;
using Blogn.Models;
using Blogn.Responses;
using Blogn.Services;
using ChaosMonkey.Guards;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blogn.Handlers
{
    public class ForgotPasswordHandler: IRequestHandler<ForgotPassword, ForgotPasswordResponse>
    {
        public ForgotPasswordHandler(ILogger<ForgotPasswordHandler> logger, IAccountRepository repository,
                                        IMailService mail, ITimeProvider time, MailSettings settings)
        {
            Logger = Guard.IsNotNull(logger, nameof(logger));
            Repository = Guard.IsNotNull(repository, nameof(repository));
            Mail = Guard.IsNotNull(mail, nameof(mail));
            Time = Guard.IsNotNull(time, nameof(time));
            Settings = Guard.IsNotNull(settings, nameof(settings));
        }

        protected ILogger Logger { get; }
        protected IAccountRepository Repository { get; }
        protected IMailService Mail { get; }
        protected ITimeProvider Time { get; }
        protected MailSettings Settings { get; }

        public async Task<ForgotPasswordResponse> Handle(ForgotPassword request, CancellationToken cancellationToken)
        {
            try
            {
                var account = await Repository.RetrieveAccountAsync(request.Email.Trim());
                if (account == null)
                {
                    Logger.LogInformation($"An attempt was made to reset the password of non-existent account '{request.Email}'.");
                    return ForgotPasswordResponse.NotFound();
                }

                var now = Time.NowUtc;
                var resetToken = new ResetToken
                {
                    Token = Guid.NewGuid().ToString(),
                    Credentials = account.Credentials,
                    DateExpired = now.AddDays(5),
                    DateCreated = now
                };
                Repository.AddResetToken(resetToken);
                await Repository.SaveAsync();
                var message = $"To reset your account password follow the link below.\r\n{request.SiteProtocol}://{request.SiteDomain}/Account/ResetPassword/{resetToken.Token}\r\n\r\nIf you did not make this request, please contact the site administrator.";
                await Mail.SendAsync(account.Email, account.DisplayName, Settings.SystemFromAddress, Settings.SystemDisplayName, 
                    "Account Reset Request", message);
                return ForgotPasswordResponse.Success();
            }
            catch (Exception ex)
            {
                Logger.LogException($"An error occurred attempting password reset for '{request.Email}'.", ex);
                return ForgotPasswordResponse.Error("An error occurred while processing your request.");
            }

        }
    }
}
