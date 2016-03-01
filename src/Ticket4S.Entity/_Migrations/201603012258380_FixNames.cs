namespace Ticket4S.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixNames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "Geo.Neighborhoods", newName: "Districts");
        }
        
        public override void Down()
        {
            RenameTable(name: "Geo.Districts", newName: "Neighborhoods");
        }
    }
}
