using System.Collections.Generic;
using Ticket4S.Entity.Evento;
using Ticket4S.Entity.Geo;
using Ticket4S.Extensions;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Ticket4S.Entity.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Ticket4SDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Ticket4SDbContext context)
        {
            SeedGeo(context);
            SeedEventosExemplos(context);
        }

        private void SeedEventosExemplos(Ticket4SDbContext context)
        {
            var evento1 = new Evento.Evento
            {
                Id = Guid.Parse("756FEF9D-25A6-4418-9910-5C5C458C94CA"),
                Nome = "Show dos 'The Pinheads'",
                NomeCurto = "The Pinheads",
                
                Habilitdo = true,
                InicioDasVendas = DateTimeOffset.Now,
                TerminoDasVendas = DateTimeOffset.Now.AddDays(60),

                CriadoEm = DateTimeOffset.Now.AddHours(-1),
                ModificadoEm = DateTimeOffset.Now.AddHours(-1),
                Local = new Local()
                {
                    Id = Guid.Parse("27655BEE-2A2A-4FE4-B035-1FF914B12C3F"),
                    Nome = "Casa de Show BTTF House RJ",
                    NomeCurto = "BTTF House",
                    UFId = "33",
                    CidadeId = "city3304557",
                    BairroId = "bairro3304557XX",
                    CEP = "24222123",
                    NomeDaRua = "Rota 395",
                    NumeroDaRua = "12",
                },
            };
            var tipoIngressos1 = new List<TipoDeIngressoDoEvento>
            {
                new TipoDeIngressoDoEvento() { Id = Guid.Parse("BEFA1122-9249-425A-9C7E-0261AA185136"),
                    EventoId = Guid.Parse("27655BEE-2A2A-4FE4-B035-1FF914B12C3F"),
                    Nome = "Palco",
                    OrdemDeExibicao = 10,
                    Valor = 88M
                },
                new TipoDeIngressoDoEvento() { Id = Guid.Parse("C3D63D54-A0E9-4A73-AEFB-7668D5ADBADB"),
                    EventoId = Guid.Parse("27655BEE-2A2A-4FE4-B035-1FF914B12C3F"),
                    Nome = "Bar",
                    OrdemDeExibicao = 20,
                    Valor = 80M
                }
            };
            evento1.TipoDeIngressos.AddAll(tipoIngressos1);

            var evento2 = new Evento.Evento
            {
                Id = Guid.Parse("C5A8E294-464F-40C5-B2A6-A4BBFF5CB825"),
                Nome = "Ultimo Show dos 'The Pinheads'",
                NomeCurto = "The Pinheads em SP",

                Habilitdo = true,
                InicioDasVendas = DateTimeOffset.Now,
                TerminoDasVendas = DateTimeOffset.Now.AddDays(60),

                CriadoEm = DateTimeOffset.Now.AddHours(-1),
                ModificadoEm = DateTimeOffset.Now.AddHours(-1),
                Local = new Local()
                {
                    Id = Guid.Parse("04BB25BB-88B1-4B83-A715-595AD5B63CAC"),
                    Nome = "Casa de Show BTTF House SP",
                    NomeCurto = "BTTF House",
                    UFId = "35",
                    CidadeId = "3303302",
                    BairroId = "3550308XX",
                    CEP = "12345678",
                    NomeDaRua = "Av Paulistana",
                    NumeroDaRua = "123",
                },
            };
            var tipoIngressos2 = new List<TipoDeIngressoDoEvento>
            {
                new TipoDeIngressoDoEvento() { Id = Guid.Parse("D7705E47-7B66-4BA0-9D13-47C1019FC917"),
                    EventoId = Guid.Parse("C5A8E294-464F-40C5-B2A6-A4BBFF5CB825"),
                    Nome = "Palco",
                    OrdemDeExibicao = 10,
                    Valor = 1050.05M
                },
                new TipoDeIngressoDoEvento() { Id = Guid.Parse("B250B16A-B346-4EBE-B9D1-751674D71333"),
                    EventoId = Guid.Parse("C5A8E294-464F-40C5-B2A6-A4BBFF5CB825"),
                    Nome = "Bar",
                    OrdemDeExibicao = 20,
                    Valor = 15000.00M
                }
            };
            evento2.TipoDeIngressos.AddAll(tipoIngressos2);


            context.Evento.AddOrUpdate(s => s.Id, evento1, evento2);
            SaveChanges(context);
        }

        private static void SeedGeo(Ticket4SDbContext context)
        {
            var stateRJ = new UF() { Id = "33", Nome = "Rio de Janeiro", Abreviacao = "RJ", ContryIsoCode = "BR", Habilitado = true };
            var stateSP = new UF() { Id = "35", Nome = "São Paulo", Abreviacao = "SP", ContryIsoCode = "BR", Habilitado = true };
            
            context.UFs.AddOrUpdate(s => s.Id, stateRJ, stateSP);
            SaveChanges(context);

            var city3304557 = new Cidade() { Id = "3304557", Nome = "Rio de Janeiro", Habilitado = true, UFId = "33" };
            var city3303302 = new Cidade() { Id = "3303302", Nome = "Niterói", Habilitado = true, UFId = "33" };
            var city355030 = new Cidade() { Id = "355030", Nome = "São Paulo", Habilitado = true, UFId = "35" };
            context.Cidades.AddOrUpdate(s => s.Id, city3304557, city3303302, city355030);
            SaveChanges(context);

            var bairro3550308XX = new Bairro() { Id = "3550308XX", Nome = "Centro", CidadeId = "355030", Habilitado = true};
            var bairro3304557XX = new Bairro() { Id = "3304557XX", Nome = "Centro", CidadeId = "3304557", Habilitado = true };
            var bairro3303302XX = new Bairro() { Id = "3303302XX", Nome = "Icaraí", CidadeId = "3303302", Habilitado = true };
            context.Bairros.AddOrUpdate(s => s.Id, bairro3550308XX, bairro3304557XX, bairro3303302XX);
            SaveChanges(context);
        }

        private static void SaveChanges(DbContext context)
        {
            context.SaveChanges();
        }

        /*
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
        */
    }
}
