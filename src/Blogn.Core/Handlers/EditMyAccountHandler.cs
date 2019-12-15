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
    public class EditMyAccountHandler: IRequestHandler<EditMyAccount, EditMyAccountResponse>
    {
        public EditMyAccountHandler(ILogger<EditMyAccountHandler> logger, IAccountRepository repository, 
                                        IAvatarService avatars, ITimeProvider time)
        {
            Logger = Guard.IsNotNull(logger, nameof(logger));
            Repository = Guard.IsNotNull(repository, nameof(repository));
            Avatars = Guard.IsNotNull(avatars, nameof(avatars));
            Time = Guard.IsNotNull(time, nameof(time));
        }

        protected ILogger Logger { get; }
        protected IAccountRepository Repository { get; }
        protected IAvatarService Avatars { get; }
        protected ITimeProvider Time { get; }

        public async Task<EditMyAccountResponse> Handle(EditMyAccount request, CancellationToken cancellationToken)
        {
            try
            {
                var account = await Repository.RetrieveAccountAsync(request.AccountId);
                if (account == null)
                {
                    Logger.LogInformation(
                        $"An attempt to change email on account '{request.AccountId}' failed because an account was not found.");
                    return EditMyAccountResponse.NotFound("Account not found.");
                }

                if (!string.Equals(request.Email, account.Email, StringComparison.CurrentCultureIgnoreCase))
                {
                    var exists = await Repository.CheckIfAccountExistsAsync(request.Email);
                    if (exists)
                    {
                        Logger.LogInformation(
                            $"An attempt to change email on account '{request.AccountId}' failed because an account with email '{request.Email}' already exist.");
                        return EditMyAccountResponse.EmailExists("The email address provided is already in use.");
                    }
                }
                account.Email = request.Email.Trim();
                account.AvatarId = Avatars.CalculateAvatarId(request.Email);
                account.DisplayName = request.DisplayName.Trim();
                account.DateUpdated = Time.NowUtc;
                await Repository.SaveAsync();
                return EditMyAccountResponse.Success();
            }
            catch (Exception ex)
            {
                Logger.LogException($"An exception occurred while attempting to edit account '{request.AccountId}'.", ex);
                return EditMyAccountResponse.ServerError("An error occurred while attempting to update your account.");
            }
        }
    }
}
