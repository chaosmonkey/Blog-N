using System.Threading.Tasks;
using AutoMapper;
using Blogn.Commands;
using Blogn.Constants;
using Blogn.Models;
using Blogn.Responses;
using Blogn.Services;
using Blogn.ViewModels;
using ChaosMonkey.Guards;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Blogn.Controllers
{
    [Authorize]
    public class AccountController: ControllerBase
    {
        public AccountController(IAuthenticationManager authenticationManager, ILogger<AccountController> logger, 
                                    IMediator mediator, IMapper mapper) :base(logger, mediator, mapper)
        {
            AuthenticationManager = Guard.IsNotNull(authenticationManager, nameof(authenticationManager));
        }

        protected IAuthenticationManager AuthenticationManager { get; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var command = new GetMyAccount{AccountId = User.GetAccountId()};
            var response = await Mediator.Send(command);

            if (response.Type == ResponseType.Success)
            {
                var model = Mapper.Map<MyAccountModel>(response.Data);
                return View(model);
            }

            return ServerError();
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var command = new GetMyAccount { AccountId = User.GetAccountId() };
            var response = await Mediator.Send(command);

            if (response.Type == ResponseType.Success)
            {
                var model = Mapper.Map<EditMyAccountModel>(response.Data);
                return View(model);
            }

            return ServerError();
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm]EditMyAccountModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var command = Mapper.Map<EditMyAccount>(model);
            command.AccountId=User.GetAccountId();

            var response  =await Mediator.Send(command);

            if (response.Type != ResponseType.Success)
            {
                ModelState.AddModelError("", response.ErrorMessage);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromForm]SignInModel model, [FromQuery] string returnUrl = "/")
        {
            if (!ModelState.IsValid) return View(model);
            var command = Mapper.Map<Authenticate>(model);

            var response = await Mediator.Send(command);

            if (!response.IsAuthenticated)
            {
                ModelState.AddModelError("", response.ErrorMessage);
                return View(model);
            }

            return await SignInAsync(response.Data, returnUrl);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<LocalRedirectResult> SignOut([FromQuery] string returnUrl = "/")
        {
            await AuthenticationManager.SignOutAsync();
            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm]RegistrationModel model, [FromQuery] string returnUrl = "/")
        {
            if (!ModelState.IsValid) return View(model);

            var command = Mapper.Map<Register>(model);
            var response = await Mediator.Send(command);

            if (!response.IsRegistered)
            {
                ModelState.AddModelError("", response.ErrorMessage);
                return View(model);
            }

            return await SignInAsync(response.Data, returnUrl);
        }

        [HttpGet]
        public ViewResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordModel model,[FromQuery] string returnUrl = "/")
        {
            if (!ModelState.IsValid) return View(model);

            var command = Mapper.Map<ChangePassword>(model);
            command.AccountId = User.GetAccountId();

            var response = await Mediator.Send(command);
            if (response.Type == ResponseType.Success)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", response.ErrorMessage);
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromForm]ForgotPasswordModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var command = Mapper.Map<ForgotPassword>(model);
            command.SiteProtocol = Request.Scheme;
            command.SiteDomain = Request.Host.ToString();
            var response = await Mediator.Send(command);

            if (response.Type == ResponseType.Error)
            {
                ModelState.AddModelError("", response.ErrorMessage);
                return View(model);
            }

            return RedirectToAction("ResetSent");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ViewResult> ResetSent([FromRoute]string id)
        {
            var command = new ValidateResetToken { Token = id };
            var response = await Mediator.Send(command);
            var model = new ValidateTokenModel
            {
                IsValid = response.Type==ResponseType.Success,
                ErrorMessage = response.ErrorMessage
            };

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetToken([FromForm]ResetPasswordModel model,[FromRoute] string id)
        {
            return View();
        }

        private async Task<IActionResult> SignInAsync(Account account, string returnUrl)
        {
            await AuthenticationManager.SignInAsync(account);
            Logger.LogInformation($"User {account.Email} has signed in.");
            return LocalRedirect(returnUrl);
        }

    }
}
