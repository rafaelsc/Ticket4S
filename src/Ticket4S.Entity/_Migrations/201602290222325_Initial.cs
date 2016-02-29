namespace Ticket4S.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Geo.Bairros",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Nome = c.String(nullable: false, maxLength: 255),
                        Habilitado = c.Boolean(nullable: false),
                        CidadeId = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Geo.Cidades", t => t.CidadeId, cascadeDelete: true)
                .Index(t => t.CidadeId);
            
            CreateTable(
                "Geo.Cidades",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Nome = c.String(nullable: false, maxLength: 255),
                        Habilitado = c.Boolean(nullable: false),
                        UFId = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Geo.UFs", t => t.UFId, cascadeDelete: true)
                .Index(t => t.UFId);
            
            CreateTable(
                "Geo.UFs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Nome = c.String(nullable: false, maxLength: 255),
                        Abreviacao = c.String(nullable: false, maxLength: 8),
                        Habilitado = c.Boolean(nullable: false),
                        ContryIsoCode = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "GatewayPagamento.CartaoDeCreditoSalvo",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IdDoCartaoNoGateway = c.String(nullable: false),
                        Bandeira = c.Int(nullable: false),
                        NumeroCartaoMascarado = c.String(nullable: false, maxLength: 24),
                        UsuarioId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nome = c.String(nullable: false),
                        CPF = c.String(nullable: false),
                        DataNascimento = c.DateTime(nullable: false),
                        Sexo = c.Int(nullable: false),
                        EnderecoId = c.Guid(),
                        _rowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("User.Endereco", t => t.EnderecoId)
                .Index(t => t.EnderecoId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "User.Endereco",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UFId = c.String(nullable: false, maxLength: 32),
                        CidadeId = c.String(nullable: false, maxLength: 32),
                        BairroId = c.String(nullable: false, maxLength: 32),
                        CEP = c.String(nullable: false, maxLength: 8),
                        NomeDaRua = c.String(nullable: false, maxLength: 256),
                        NumeroDaRua = c.String(nullable: false, maxLength: 32),
                        ComplementoDaRua = c.String(maxLength: 128),
                        _rowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Geo.Bairros", t => t.BairroId, cascadeDelete: false)
                .ForeignKey("Geo.Cidades", t => t.CidadeId, cascadeDelete: false)
                .ForeignKey("Geo.UFs", t => t.UFId, cascadeDelete: false)
                .Index(t => t.UFId)
                .Index(t => t.CidadeId)
                .Index(t => t.BairroId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "Evento.Eventos",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 256),
                        NomeCurto = c.String(nullable: false, maxLength: 32),
                        LocalId = c.Guid(nullable: false),
                        InicioDasVendas = c.DateTimeOffset(nullable: false, precision: 7),
                        TerminoDasVendas = c.DateTimeOffset(precision: 7),
                        Habilitdo = c.Boolean(nullable: false),
                        _criadoEm = c.DateTimeOffset(nullable: false, precision: 7),
                        _modificadoEm = c.DateTimeOffset(nullable: false, precision: 7),
                        _rowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Evento.Locais", t => t.LocalId, cascadeDelete: true)
                .Index(t => t.LocalId);
            
            CreateTable(
                "Evento.Locais",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 256),
                        NomeCurto = c.String(nullable: false, maxLength: 32),
                        UFId = c.String(nullable: false, maxLength: 32),
                        CidadeId = c.String(nullable: false, maxLength: 32),
                        BairroId = c.String(nullable: false, maxLength: 32),
                        CEP = c.String(nullable: false, maxLength: 8),
                        NomeDaRua = c.String(maxLength: 256),
                        NumeroDaRua = c.String(maxLength: 32),
                        ComplementoDaRua = c.String(maxLength: 128),
                        _rowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Geo.Bairros", t => t.BairroId, cascadeDelete: false)
                .ForeignKey("Geo.Cidades", t => t.CidadeId, cascadeDelete: false)
                .ForeignKey("Geo.UFs", t => t.UFId, cascadeDelete: false)
                .Index(t => t.UFId)
                .Index(t => t.CidadeId)
                .Index(t => t.BairroId);
            
            CreateTable(
                "Evento.TiposDeIngressoDoEvento",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 256),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrdemDeExibicao = c.Byte(nullable: false),
                        Habilitado = c.Boolean(nullable: false),
                        EventoId = c.Guid(nullable: false),
                        _rowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Evento.Eventos", t => t.EventoId, cascadeDelete: true)
                .Index(t => t.EventoId);
            
            CreateTable(
                "GatewayPagamento.HistoricoDeTransacoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DataHoraEvento = c.DateTimeOffset(nullable: false, precision: 7),
                        UsuarioId = c.String(maxLength: 128),
                        EventoAdquiridoId = c.Guid(),
                        IngressoAdquiridoId = c.Guid(),
                        SolicitadoSalvarOCartaoDeCredito = c.Boolean(nullable: false),
                        PagamentoCobradoComSucesso = c.Boolean(nullable: false),
                        IdDoPedidoNoSistemaDePagamento = c.String(),
                        MessagemDeRespostaDaOperacao = c.String(maxLength: 512),
                        DebugRawData = c.String(),
                        _criadoEm = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Evento.Eventos", t => t.EventoAdquiridoId)
                .ForeignKey("Evento.TiposDeIngressoDoEvento", t => t.IngressoAdquiridoId)
                .ForeignKey("dbo.AspNetUsers", t => t.UsuarioId)
                .Index(t => t.DataHoraEvento)
                .Index(t => t.UsuarioId)
                .Index(t => t.EventoAdquiridoId)
                .Index(t => t.IngressoAdquiridoId);
            
            CreateTable(
                "Compras.PedidosDeCompras",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DataHoraEvento = c.DateTimeOffset(nullable: false, precision: 7),
                        UsuarioId = c.String(maxLength: 128),
                        EventoAdquiridoId = c.Guid(),
                        IngressoAdquiridoId = c.Guid(),
                        ValorCobrado = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Situacao = c.Int(nullable: false),
                        _criadoEm = c.DateTimeOffset(nullable: false, precision: 7),
                        _modificadoEm = c.DateTimeOffset(nullable: false, precision: 7),
                        _rowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Evento.Eventos", t => t.EventoAdquiridoId)
                .ForeignKey("Evento.TiposDeIngressoDoEvento", t => t.IngressoAdquiridoId)
                .ForeignKey("dbo.AspNetUsers", t => t.UsuarioId)
                .Index(t => t.DataHoraEvento)
                .Index(t => t.UsuarioId)
                .Index(t => t.EventoAdquiridoId)
                .Index(t => t.IngressoAdquiridoId)
                .Index(t => t.Situacao);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("Compras.PedidosDeCompras", "UsuarioId", "dbo.AspNetUsers");
            DropForeignKey("Compras.PedidosDeCompras", "IngressoAdquiridoId", "Evento.TiposDeIngressoDoEvento");
            DropForeignKey("Compras.PedidosDeCompras", "EventoAdquiridoId", "Evento.Eventos");
            DropForeignKey("GatewayPagamento.HistoricoDeTransacoes", "UsuarioId", "dbo.AspNetUsers");
            DropForeignKey("GatewayPagamento.HistoricoDeTransacoes", "IngressoAdquiridoId", "Evento.TiposDeIngressoDoEvento");
            DropForeignKey("GatewayPagamento.HistoricoDeTransacoes", "EventoAdquiridoId", "Evento.Eventos");
            DropForeignKey("Evento.TiposDeIngressoDoEvento", "EventoId", "Evento.Eventos");
            DropForeignKey("Evento.Eventos", "LocalId", "Evento.Locais");
            DropForeignKey("Evento.Locais", "UFId", "Geo.UFs");
            DropForeignKey("Evento.Locais", "CidadeId", "Geo.Cidades");
            DropForeignKey("Evento.Locais", "BairroId", "Geo.Bairros");
            DropForeignKey("GatewayPagamento.CartaoDeCreditoSalvo", "UsuarioId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "EnderecoId", "User.Endereco");
            DropForeignKey("User.Endereco", "UFId", "Geo.UFs");
            DropForeignKey("User.Endereco", "CidadeId", "Geo.Cidades");
            DropForeignKey("User.Endereco", "BairroId", "Geo.Bairros");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("Geo.Bairros", "CidadeId", "Geo.Cidades");
            DropForeignKey("Geo.Cidades", "UFId", "Geo.UFs");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("Compras.PedidosDeCompras", new[] { "Situacao" });
            DropIndex("Compras.PedidosDeCompras", new[] { "IngressoAdquiridoId" });
            DropIndex("Compras.PedidosDeCompras", new[] { "EventoAdquiridoId" });
            DropIndex("Compras.PedidosDeCompras", new[] { "UsuarioId" });
            DropIndex("Compras.PedidosDeCompras", new[] { "DataHoraEvento" });
            DropIndex("GatewayPagamento.HistoricoDeTransacoes", new[] { "IngressoAdquiridoId" });
            DropIndex("GatewayPagamento.HistoricoDeTransacoes", new[] { "EventoAdquiridoId" });
            DropIndex("GatewayPagamento.HistoricoDeTransacoes", new[] { "UsuarioId" });
            DropIndex("GatewayPagamento.HistoricoDeTransacoes", new[] { "DataHoraEvento" });
            DropIndex("Evento.TiposDeIngressoDoEvento", new[] { "EventoId" });
            DropIndex("Evento.Locais", new[] { "BairroId" });
            DropIndex("Evento.Locais", new[] { "CidadeId" });
            DropIndex("Evento.Locais", new[] { "UFId" });
            DropIndex("Evento.Eventos", new[] { "LocalId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("User.Endereco", new[] { "BairroId" });
            DropIndex("User.Endereco", new[] { "CidadeId" });
            DropIndex("User.Endereco", new[] { "UFId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "EnderecoId" });
            DropIndex("GatewayPagamento.CartaoDeCreditoSalvo", new[] { "UsuarioId" });
            DropIndex("Geo.Cidades", new[] { "UFId" });
            DropIndex("Geo.Bairros", new[] { "CidadeId" });
            DropTable("dbo.AspNetRoles");
            DropTable("Compras.PedidosDeCompras");
            DropTable("GatewayPagamento.HistoricoDeTransacoes");
            DropTable("Evento.TiposDeIngressoDoEvento");
            DropTable("Evento.Locais");
            DropTable("Evento.Eventos");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("User.Endereco");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("GatewayPagamento.CartaoDeCreditoSalvo");
            DropTable("Geo.UFs");
            DropTable("Geo.Cidades");
            DropTable("Geo.Bairros");
        }
    }
}
