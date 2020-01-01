using Autofac.Extras.Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Blogn.Tests.Controllers
{
    public abstract class ControllerTestBase<TController> where TController: Blogn.Controllers.ControllerBase
    {
        protected ControllerTestBase()
        {
            Mocks = AutoMock.GetLoose();
            MockContext = Mocks.Mock<HttpContext>();
            Controller = Mocks.Create<TController>();
            Controller.ControllerContext = new ControllerContext
            {
                HttpContext = MockContext.Object
            };
        }

        public virtual void Dispose()
        {
            Controller?.Dispose();
        }

        protected AutoMock Mocks { get; }
        protected TController Controller { get; }
        protected Mock<HttpContext> MockContext { get; }

    }
}
