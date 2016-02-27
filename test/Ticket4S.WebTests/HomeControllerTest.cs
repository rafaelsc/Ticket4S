using System;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions.Mvc;
using FluentAssertions;
using Ticket4S.Web.Controllers;
using Xunit;

namespace Ticket4S.WebTests
{
    public class HomeControllerTest
    {
        [Fact]
        public void HomeDeveTrazerListaDeEventos()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index();

            // Assert
            result.Should().BeViewResult();
        }
    }
}
