using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions.Mvc;
using FluentAssertions;
using Ticket4S.Entity;
using Ticket4S.Entity.Event;
using Ticket4S.Extensions;
using Ticket4S.Web.Controllers;
using Xunit;
using Moq;
using EntityFramework.Testing;

namespace Ticket4S.WebTests
{
    public class HomeControllerTest
    {
        private IEnumerable<Event> SampleData()
        {
            Guid lastId;

            yield return new Event
            {
                Id = lastId = Guid.NewGuid(),
                Name = "Show dos 'The Pinheads'",
                ShortName = "The Pinheads",

                Active = true,
                BeginningOfSales = DateTimeOffset.Now,
                EndOfSales = DateTimeOffset.Now.AddDays(60),

                TicketsTypes = new List<EventTicketType>()
                {
                    new EventTicketType() {
                        Id = Guid.NewGuid(),
                        EventId = lastId,
                        Name = "Palco",
                        ViewOrder = 10,
                        Value = 88M
                    },
                    new EventTicketType() {
                        Id = Guid.NewGuid(),
                        EventId = lastId,
                        Name = "Bar",
                        ViewOrder = 20,
                        Value = 80M
                    }
                }
            };

            yield return new Event
            {
                Id = lastId = Guid.NewGuid(),
                Name = "Ultimo Show dos 'The Pinheads'",
                ShortName = "The Pinheads em SP",

                Active = true,
                BeginningOfSales = DateTimeOffset.Now,
                EndOfSales = DateTimeOffset.Now.AddDays(60),
              
                TicketsTypes = new List<EventTicketType>
                {
                    new EventTicketType() {
                        Id = Guid.NewGuid(),
                        EventId = lastId,
                        Name = "Palco",
                        ViewOrder = 10,
                        Value = 1050.05M
                    },
                    new EventTicketType() {
                        Id = Guid.NewGuid(),
                        EventId = lastId,
                        Name = "Bar",
                        ViewOrder = 20,
                        Value = 15000.00M
                    }
                }
            };

            yield return new Event
            {
                Id = lastId = Guid.NewGuid(),
                Name = "Show que Nunca Irá existir",
                ShortName = "NoShow",

                Active = false,
                BeginningOfSales = DateTimeOffset.Now,
                EndOfSales = DateTimeOffset.Now.AddDays(60),

                TicketsTypes = new List<EventTicketType>
                {
                    new EventTicketType() {
                        Id = Guid.NewGuid(),
                        EventId = lastId,
                        Name = "Palco",
                        ViewOrder = 10,
                        Value = 1050.05M
                    },
                    new EventTicketType() {
                        Id = Guid.NewGuid(),
                        EventId = lastId,
                        Name = "Bar",
                        ViewOrder = 20,
                        Value = 15000.00M
                    }
                }
            };

            yield return new Event
            {
                Id = lastId = Guid.NewGuid(),
                Name = "Show que ainda Irá acontecer",
                ShortName = "FutureShow",

                Active = true,
                BeginningOfSales = DateTimeOffset.Now.AddDays(10),
                EndOfSales = DateTimeOffset.Now.AddDays(60),

                TicketsTypes = new List<EventTicketType>
                {
                    new EventTicketType() {
                        Id = Guid.NewGuid(),
                        EventId = lastId,
                        Name = "Palco",
                        ViewOrder = 10,
                        Value = 1050.05M
                    },
                    new EventTicketType() {
                        Id = Guid.NewGuid(),
                        EventId = lastId,
                        Name = "Bar",
                        ViewOrder = 20,
                        Value = 15000.00M
                    }
                }
            };

            yield return new Event
            {
                Id = lastId = Guid.NewGuid(),
                Name = "Show que ainda Irá acontecer",
                ShortName = "FutureShow",

                Active = true,
                BeginningOfSales = DateTimeOffset.Now.AddDays(10),
                EndOfSales = DateTimeOffset.Now.AddDays(60),

                TicketsTypes = new List<EventTicketType>
                {
                    new EventTicketType() {
                        Id =  Guid.NewGuid(),
                        EventId = lastId,
                        Name = "Palco",
                        ViewOrder = 10,
                        Value = 1050.05M
                    },
                    new EventTicketType() {
                        Id =  Guid.NewGuid(),
                        EventId = lastId,
                        Name = "Bar",
                        ViewOrder = 20,
                        Value = 15000.00M
                    }
                }
            };
        }

        [Fact]
        public void HomeDeveTrazerListaDeEventos()
        {
            // Arrange
            // Create a mock set and context
            var set = new Mock<DbSet<Event>>().SetupData(SampleData().ToList());
            var context = new Mock<Ticket4SDbContext>();
            context.Setup(c => c.Event).Returns(set.Object);

            var controller = new HomeController(context.Object); //todo: FakeContext

            // Act
            var result = controller.Index();

            // Assert
            result.Should().BeViewResult();
            result.Model.Should().NotBeNull();
            result.Model.Should().BeOfType<List<Event>>();
            var viewModel = result.Model as List<Event>;
            viewModel.Should().NotBeEmpty();
            viewModel.Should().OnlyHaveUniqueItems();
            viewModel.Should().HaveCount(2);
            viewModel.Should().NotContainNulls();
            viewModel.Should().NotContain(e => e.Active == false);
            viewModel.Should().NotContain(e => e.BeginningOfSales > DateTimeOffset.Now);
            viewModel.Should().NotContain(e => e.EndOfSales < DateTimeOffset.Now);
            viewModel.Should().NotContain(e => e.TicketsTypes.Count == 0);
        }
    }
}
