using System.Collections.Generic;
using Ticket4S.Entity.Event;
using Ticket4S.Entity.Geo;
using Ticket4S.Extensions;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Text;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ticket4S.Entity.User;

namespace Ticket4S.Entity.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Ticket4SDbContext> //DropCreateDatabaseAlways<Ticket4SDbContext> 
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"_Migrations";
        }

        protected override void Seed(Ticket4SDbContext context)
        {
            SeedGeo(context);
            SeedEventosExemplos(context);
            SeedUser(context);
        }

        private void SeedUser(Ticket4SDbContext context)
        {
            var roleAdm = new IdentityRole { Id = "admin", Name = "Administrators" };
            var roleCli = new IdentityRole { Id = "customer", Name = "Customers" };
            context.Roles.AddOrUpdate(r => r.Id, roleAdm, roleCli);
            SaveChanges(context);


            var userAdminstrator = new User.User
            {
                Id = "91965C7B-78BF-400A-98A8-64BABAE18218",
                UserName = "admin",
                PasswordHash = (new PasswordHasher().HashPassword("admin123456")),
                SecurityStamp = "",
                Email = "mail@rafaelsc.net",
                EmailConfirmed = true,
                PhoneNumber = "5521988585900",
                PhoneNumberConfirmed = true,
                Roles =
                {
                    new IdentityUserRole() {RoleId = "admin", UserId = "91965C7B-78BF-400A-98A8-64BABAE18218"},
                    new IdentityUserRole() {RoleId = "customer", UserId = "91965C7B-78BF-400A-98A8-64BABAE18218"},
                },
                Name = "Rafael Cordiero",
                CPF = "65130660340", //Não é o Meu
                DateOfBirth = new DateTime(1980,04,10),
                Gender = Gender.Male,
                Address =
                    new Address()
                    {
                        Id = Guid.NewGuid(),
                        StateId = "33",
                        CityId = "3303302",
                        DistrictId = "3303302XX",
                        ZipCode = "24230000",
                        Street = "Av. Roberto Silvera",
                        StreetNumber = "000",
                        StreetComplement = "Escritorio"
                    }
            };
            context.Users.AddOrUpdate(u => u.Id, userAdminstrator);
            SaveChanges(context);
        }

        private void SeedEventosExemplos(Ticket4SDbContext context)
        {
            var evento1 = new Event.Event
            {
                Id = Guid.Parse("756FEF9D-25A6-4418-9910-5C5C458C94CA"),
                Name = "Show dos 'The Pinheads' no Rio",
                ShortName = "The Pinheads",
                
                Active = true,
                BeginningOfSales = DateTimeOffset.Now,
                EndOfSales = DateTimeOffset.Now.AddDays(60),

                CreatedAt = DateTimeOffset.Now.AddHours(-1),
                ChangedAt = DateTimeOffset.Now.AddHours(-1),
                EventPlaceId = Guid.Parse("27655BEE-2A2A-4FE4-B035-1FF914B12C3F"),
                EventPlace = new EventPlace()
                {
                    Id = Guid.Parse("27655BEE-2A2A-4FE4-B035-1FF914B12C3F"),
                    Name = "Casa de Show BTTF House RJ",
                    ShortName = "BTTF House",
                    StateId = "33",
                    CityId = "3304557",
                    DistrictId = "3304557XX",
                    ZipCode = "24222123",
                    Street = "Rota 395",
                    StreetNumber = "12",
                },
            };
            var tipoIngressos1 = new List<EventTicketType>
            {
                new EventTicketType() { Id = Guid.Parse("BEFA1122-9249-425A-9C7E-0261AA185136"),
                    EventId = Guid.Parse("27655BEE-2A2A-4FE4-B035-1FF914B12C3F"),
                    Name = "Palco",
                    ViewOrder = 10,
                    Value = 88M
                },
                new EventTicketType() { Id = Guid.Parse("C3D63D54-A0E9-4A73-AEFB-7668D5ADBADB"),
                    EventId = Guid.Parse("27655BEE-2A2A-4FE4-B035-1FF914B12C3F"),
                    Name = "Bar",
                    ViewOrder = 20,
                    Value = 80M
                }
            };
            evento1.TicketsTypes.AddAll(tipoIngressos1);

            var evento2 = new Event.Event
            {
                Id = Guid.Parse("C5A8E294-464F-40C5-B2A6-A4BBFF5CB825"),
                Name = "Ultimo Show dos 'The Pinheads' em São Paulo",
                ShortName = "The Pinheads em SP",

                Active = true,
                BeginningOfSales = DateTimeOffset.Now,
                EndOfSales = DateTimeOffset.Now.AddDays(60),

                CreatedAt = DateTimeOffset.Now.AddHours(-1),
                ChangedAt = DateTimeOffset.Now.AddHours(-1),
                EventPlaceId = Guid.Parse("04BB25BB-88B1-4B83-A715-595AD5B63CAC"),
                EventPlace = new EventPlace()
                {
                    Id = Guid.Parse("04BB25BB-88B1-4B83-A715-595AD5B63CAC"),
                    Name = "Casa de Show BTTF House SP",
                    ShortName = "BTTF House",
                    StateId = "35",
                    CityId = "3303302",
                    DistrictId = "3550308XX",
                    ZipCode = "12345678",
                    Street = "Av Paulistana",
                    StreetNumber = "123",
                },
            };
            var tipoIngressos2 = new List<EventTicketType>
            {
                new EventTicketType() { Id = Guid.Parse("D7705E47-7B66-4BA0-9D13-47C1019FC917"),
                    EventId = Guid.Parse("C5A8E294-464F-40C5-B2A6-A4BBFF5CB825"),
                    Name = "Palco",
                    ViewOrder = 10,
                    Value = 1050.05M
                },
                new EventTicketType() { Id = Guid.Parse("B250B16A-B346-4EBE-B9D1-751674D71333"),
                    EventId = Guid.Parse("C5A8E294-464F-40C5-B2A6-A4BBFF5CB825"),
                    Name = "Bar",
                    ViewOrder = 20,
                    Value = 15000.00M
                }
            };
            evento2.TicketsTypes.AddAll(tipoIngressos2);


            context.Event.AddOrUpdate(s => s.Id, evento1, evento2);
            SaveChanges(context);
        }

        private static void SeedGeo(Ticket4SDbContext context)
        {
            var stateRJ = new State() { Id = "33", Name = "Rio de Janeiro", Abbreviation = "RJ", ContryIsoCode = "BR", Available = true };
            var stateSP = new State() { Id = "35", Name = "São Paulo", Abbreviation = "SP", ContryIsoCode = "BR", Available = true };
            
            context.States.AddOrUpdate(s => s.Id, stateRJ, stateSP);
            SaveChanges(context);

            var city3304557 = new City() { Id = "3304557", Name = "Rio de Janeiro", Available = true, StateId = "33" };
            var city3303302 = new City() { Id = "3303302", Name = "Niterói", Available = true, StateId = "33" };
            var city355030 = new City() { Id = "355030", Name = "São Paulo", Available = true, StateId = "35" };
            context.Cities.AddOrUpdate(s => s.Id, city3304557, city3303302, city355030);
            SaveChanges(context);

            var bairro3550308XX = new District() { Id = "3550308XX", Name = "Centro", CityId = "355030", Available = true};
            var bairro3304557XX = new District() { Id = "3304557XX", Name = "Centro", CityId = "3304557", Available = true };
            var bairro3303302XX = new District() { Id = "3303302XX", Name = "Icaraí", CityId = "3303302", Available = true };
            context.Neighborhoods.AddOrUpdate(s => s.Id, bairro3550308XX, bairro3304557XX, bairro3303302XX);
            SaveChanges(context);
        }

        /*
        private static void SaveChanges(DbContext context)
        {
            context.SaveChanges();
        }
        */

        
        private static void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                //if (System.Diagnostics.Debugger.IsAttached == false)
                    //System.Diagnostics.Debugger.Launch();

                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} (Base: {1}) - Failed validation\n", failure.Entry.Entity.GetType(), failure.Entry.Entity.GetType().BaseType);
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
                throw new DbEntityValidationException("Entity Validation Failed - errors follow:\n" + sb.ToString(), ex); // Add the original exception as the innerException
            }
        }
        
    }
}
