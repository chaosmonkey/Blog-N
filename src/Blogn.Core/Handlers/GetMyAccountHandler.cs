using System;
using System.Threading;
using System.Threading.Tasks;
using Blogn.Commands;
using Blogn.Data;
using Blogn.Responses;
using ChaosMonkey.Guards;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blogn.Handlers
{
    public class GetMyAccountHandler: IRequestHandler<GetMyAccount, MyAccountResponse>
    {
        public GetMyAccountHandler(ILogger<GetMyAccountHandler> logger, IAccountRepository repository)
        {
            Logger = Guard.IsNotNull(logger, nameof(logger));
            Repository = Guard.IsNotNull(repository, nameof(repository));
        }

        protected ILogger Logger { get; }
        protected IAccountRepository Repository { get; }

        public async Task<MyAccountResponse> Handle(GetMyAccount request, CancellationToken cancellationToken)
        {
            try
            {
                var account = await Repository.RetrieveAccountAsync(request.AccountId);
                if (account == null)
                {
                    return MyAccountResponse.NotFound("Your account was not found! Please contact the administrator for assistance.");
                }
                return MyAccountResponse.Success(account);
            }
            catch (Exception ex)
            {
                Logger.LogException($"An error occurred while retrieving account {request.AccountId}.", ex);
                return MyAccountResponse.Error("An error occurred while retrieving your account.");
            }
        }
    }
}
