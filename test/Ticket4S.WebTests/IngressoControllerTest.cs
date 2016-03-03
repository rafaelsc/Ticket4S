using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions.Mvc;
using FluentAssertions;
using Ticket4S.Entity;
using Ticket4S.Entity.Event;
using Ticket4S.Web.Controllers;
using Xunit;
using Moq;
using Ticket4S.Web;
using Ticket4S.Web.ViewModels;

namespace Ticket4S.WebTests
{
    public class IngressoControllerTest
    {
        private MapperConfiguration _mapperConfiguration;

        public IngressoControllerTest()
        {
            _mapperConfiguration = AutoMapperConfig.Config();
        }

    }
}
