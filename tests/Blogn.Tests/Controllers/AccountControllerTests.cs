using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using AutoMapper;
using Blogn.Commands;
using Blogn.Controllers;
using Blogn.Responses;
using Blogn.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Blogn.Tests.Controllers
{
    public class AccountControllerTests: ControllerTestBase<AccountController>
    {
        public AccountControllerTests()
        {
            _mockMediator = Mocks.Mock<IMediator>();
            _mockMapper = Mocks.Mock<IMapper>();
        }
        
        public override void Dispose()
        {
            base.Dispose();
        }

        private Mock<IMediator> _mockMediator;
        private Mock<IMapper> _mockMapper;

        [Fact]
        public async Task ForgotPassword_WhenModelStateInValid_ReturnsExpectedView()
        {
            Controller.ModelState.AddModelError("", "An Error Occurred");

            var result = await Controller.ForgotPassword(new ForgotPasswordModel());

            Assert.IsAssignableFrom<ViewResult>(result);
            var view = result as ViewResult;
            Assert.Null(view.ViewName);
        }

        [Fact]
        public async Task ForgotPassword_SetsCommandSiteProtocol()
        {
            var model = new ForgotPasswordModel
            {
                Email = "test@example.com"
            };
            var host = new HostString("localhost", 8080);
            var mockRequest = Mocks.Mock<HttpRequest>();
            mockRequest.Setup(mock => mock.Host).Returns(host);
            mockRequest.Setup(mock => mock.Scheme).Returns("https");
            MockContext.Setup(mock => mock.Request).Returns(mockRequest.Object);
            _mockMapper.Setup(mock => mock.Map<ForgotPassword>(model)).Returns(new ForgotPassword { Email = model.Email });
            _mockMediator.Setup(mock => mock.Send(It.IsAny<ForgotPassword>(), default))
                .Returns(Task.FromResult(ForgotPasswordResponse.NotFound()));

            await Controller.ForgotPassword(model);

            _mockMediator.Verify(mock => mock.Send(
                It.Is<ForgotPassword>(command => command.SiteDomain == "localhost:8080" 
                                    && command.SiteProtocol == "https"), default), Times.Once);
        }
    }
}
