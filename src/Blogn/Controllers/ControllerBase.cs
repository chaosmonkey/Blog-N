using AutoMapper;
using Blogn.Constants;
using ChaosMonkey.Guards;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blogn.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected ControllerBase(ILogger logger, IMediator mediator, IMapper mapper)
        {
            Logger = Guard.IsNotNull(logger, nameof(logger));
            Mediator = Guard.IsNotNull(mediator, nameof(mediator));
            Mapper = Guard.IsNotNull(mapper, nameof(mapper));
        }

        protected IMediator Mediator { get; }
        protected ILogger Logger { get; }
        protected IMapper Mapper { get; }

        public LocalRedirectResult ServerError()
        {
            return LocalRedirect(WellKnownRoute.ServerError);
        }
    }
}
