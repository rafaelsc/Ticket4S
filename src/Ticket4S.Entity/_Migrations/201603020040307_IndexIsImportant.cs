namespace Ticket4S.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IndexIsImportant : DbMigration
    {
        public override void Up()
        {
            CreateIndex("Event.Events", "ShortName");
            CreateIndex("Event.Events", new[] { "Active", "BeginningOfSales", "EndOfSales" }, name: "periodOfSales");
            CreateIndex("Event.EventTicketTypes", "Available");
        }
        
        public override void Down()
        {
            DropIndex("Event.EventTicketTypes", new[] { "Available" });
            DropIndex("Event.Events", "periodOfSales");
            DropIndex("Event.Events", new[] { "ShortName" });
        }
    }
}
