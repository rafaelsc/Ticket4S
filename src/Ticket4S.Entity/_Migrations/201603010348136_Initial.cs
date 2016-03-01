namespace Ticket4S.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "User.Addresses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StateId = c.String(nullable: false, maxLength: 32),
                        CityId = c.String(nullable: false, maxLength: 32),
                        DistrictId = c.String(nullable: false, maxLength: 32),
                        ZipCode = c.String(nullable: false, maxLength: 8),
                        Street = c.String(nullable: false, maxLength: 256),
                        StreetNumber = c.String(nullable: false, maxLength: 32),
                        StreetComplement = c.String(maxLength: 128),
                        _rowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Geo.Cities", t => t.CityId, cascadeDelete: false)
                .ForeignKey("Geo.Neighborhoods", t => t.DistrictId, cascadeDelete: false)
                .ForeignKey("Geo.States", t => t.StateId, cascadeDelete: false)
                .Index(t => t.StateId)
                .Index(t => t.CityId)
                .Index(t => t.DistrictId);
            
            CreateTable(
                "Geo.Cities",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Name = c.String(nullable: false, maxLength: 255),
                        Available = c.Boolean(nullable: false),
                        StateId = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Geo.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId);
            
            CreateTable(
                "Geo.Neighborhoods",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Name = c.String(nullable: false, maxLength: 255),
                        Available = c.Boolean(nullable: false),
                        CityId = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Geo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "Geo.States",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Name = c.String(nullable: false, maxLength: 255),
                        Abbreviation = c.String(nullable: false, maxLength: 8),
                        Available = c.Boolean(nullable: false),
                        ContryIsoCode = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "GatewayPayment.SavedCreditCards",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IdOfSavedCardInTheGateway = c.String(nullable: false),
                        CreditCardBrand = c.Int(nullable: false),
                        MaskedCreditCardNumber = c.String(nullable: false, maxLength: 24),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        CPF = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                        AddressId = c.Guid(),
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
                .ForeignKey("User.Addresses", t => t.AddressId)
                .Index(t => t.AddressId)
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
                "Event.Events",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        ShortName = c.String(nullable: false, maxLength: 32),
                        EventPlaceId = c.Guid(nullable: false),
                        BeginningOfSales = c.DateTimeOffset(nullable: false, precision: 7),
                        EndOfSales = c.DateTimeOffset(precision: 7),
                        Active = c.Boolean(nullable: false),
                        _createdAt = c.DateTimeOffset(nullable: false, precision: 7),
                        _changedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        _rowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Event.Places", t => t.EventPlaceId, cascadeDelete: true)
                .Index(t => t.EventPlaceId);
            
            CreateTable(
                "Event.Places",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        ShortName = c.String(nullable: false, maxLength: 32),
                        StateId = c.String(nullable: false, maxLength: 32),
                        CityId = c.String(nullable: false, maxLength: 32),
                        DistrictId = c.String(nullable: false, maxLength: 32),
                        ZipCode = c.String(nullable: false, maxLength: 8),
                        Street = c.String(maxLength: 256),
                        StreetNumber = c.String(maxLength: 32),
                        StreetComplement = c.String(maxLength: 128),
                        _rowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Geo.Cities", t => t.CityId, cascadeDelete: false)
                .ForeignKey("Geo.Neighborhoods", t => t.DistrictId, cascadeDelete: false)
                .ForeignKey("Geo.States", t => t.StateId, cascadeDelete: false)
                .Index(t => t.StateId)
                .Index(t => t.CityId)
                .Index(t => t.DistrictId);
            
            CreateTable(
                "Event.EventTicketTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ViewOrder = c.Byte(nullable: false),
                        Available = c.Boolean(nullable: false),
                        EventId = c.Guid(nullable: false),
                        _rowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Event.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
            CreateTable(
                "GatewayPayment.TransactionHistory",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TransactionDateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        BuyerUserId = c.String(maxLength: 128),
                        BoughtEventId = c.Guid(),
                        BoughtTicketId = c.Guid(),
                        BilledValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserRequestToSaveCreditCard = c.Boolean(nullable: false),
                        PaymentBilledSuccessful = c.Boolean(nullable: false),
                        OrderIdInTheGatewaySystem = c.String(),
                        OperationMessage = c.String(maxLength: 512),
                        DebugRawData = c.String(),
                        _createdAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Event.Events", t => t.BoughtEventId)
                .ForeignKey("Event.EventTicketTypes", t => t.BoughtTicketId)
                .ForeignKey("dbo.AspNetUsers", t => t.BuyerUserId)
                .Index(t => t.TransactionDateTime)
                .Index(t => t.BuyerUserId)
                .Index(t => t.BoughtEventId)
                .Index(t => t.BoughtTicketId);
            
            CreateTable(
                "Purchase.PurchaseOrders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderDateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        BuyerUserId = c.String(maxLength: 128),
                        BoughtEventId = c.Guid(),
                        BoughtTicketId = c.Guid(),
                        BilledValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Condition = c.Int(nullable: false),
                        _createdAt = c.DateTimeOffset(nullable: false, precision: 7),
                        _changedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        _rowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Event.Events", t => t.BoughtEventId)
                .ForeignKey("Event.EventTicketTypes", t => t.BoughtTicketId)
                .ForeignKey("dbo.AspNetUsers", t => t.BuyerUserId)
                .Index(t => t.OrderDateTime)
                .Index(t => t.BuyerUserId)
                .Index(t => t.BoughtEventId)
                .Index(t => t.BoughtTicketId)
                .Index(t => t.Condition);
            
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
            DropForeignKey("Purchase.PurchaseOrders", "BuyerUserId", "dbo.AspNetUsers");
            DropForeignKey("Purchase.PurchaseOrders", "BoughtTicketId", "Event.EventTicketTypes");
            DropForeignKey("Purchase.PurchaseOrders", "BoughtEventId", "Event.Events");
            DropForeignKey("GatewayPayment.TransactionHistory", "BuyerUserId", "dbo.AspNetUsers");
            DropForeignKey("GatewayPayment.TransactionHistory", "BoughtTicketId", "Event.EventTicketTypes");
            DropForeignKey("GatewayPayment.TransactionHistory", "BoughtEventId", "Event.Events");
            DropForeignKey("Event.EventTicketTypes", "EventId", "Event.Events");
            DropForeignKey("Event.Events", "EventPlaceId", "Event.Places");
            DropForeignKey("Event.Places", "StateId", "Geo.States");
            DropForeignKey("Event.Places", "DistrictId", "Geo.Neighborhoods");
            DropForeignKey("Event.Places", "CityId", "Geo.Cities");
            DropForeignKey("GatewayPayment.SavedCreditCards", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "AddressId", "User.Addresses");
            DropForeignKey("User.Addresses", "StateId", "Geo.States");
            DropForeignKey("User.Addresses", "DistrictId", "Geo.Neighborhoods");
            DropForeignKey("User.Addresses", "CityId", "Geo.Cities");
            DropForeignKey("Geo.Cities", "StateId", "Geo.States");
            DropForeignKey("Geo.Neighborhoods", "CityId", "Geo.Cities");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("Purchase.PurchaseOrders", new[] { "Condition" });
            DropIndex("Purchase.PurchaseOrders", new[] { "BoughtTicketId" });
            DropIndex("Purchase.PurchaseOrders", new[] { "BoughtEventId" });
            DropIndex("Purchase.PurchaseOrders", new[] { "BuyerUserId" });
            DropIndex("Purchase.PurchaseOrders", new[] { "OrderDateTime" });
            DropIndex("GatewayPayment.TransactionHistory", new[] { "BoughtTicketId" });
            DropIndex("GatewayPayment.TransactionHistory", new[] { "BoughtEventId" });
            DropIndex("GatewayPayment.TransactionHistory", new[] { "BuyerUserId" });
            DropIndex("GatewayPayment.TransactionHistory", new[] { "TransactionDateTime" });
            DropIndex("Event.EventTicketTypes", new[] { "EventId" });
            DropIndex("Event.Places", new[] { "DistrictId" });
            DropIndex("Event.Places", new[] { "CityId" });
            DropIndex("Event.Places", new[] { "StateId" });
            DropIndex("Event.Events", new[] { "EventPlaceId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "AddressId" });
            DropIndex("GatewayPayment.SavedCreditCards", new[] { "UserId" });
            DropIndex("Geo.Neighborhoods", new[] { "CityId" });
            DropIndex("Geo.Cities", new[] { "StateId" });
            DropIndex("User.Addresses", new[] { "DistrictId" });
            DropIndex("User.Addresses", new[] { "CityId" });
            DropIndex("User.Addresses", new[] { "StateId" });
            DropTable("dbo.AspNetRoles");
            DropTable("Purchase.PurchaseOrders");
            DropTable("GatewayPayment.TransactionHistory");
            DropTable("Event.EventTicketTypes");
            DropTable("Event.Places");
            DropTable("Event.Events");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("GatewayPayment.SavedCreditCards");
            DropTable("Geo.States");
            DropTable("Geo.Neighborhoods");
            DropTable("Geo.Cities");
            DropTable("User.Addresses");
        }
    }
}
