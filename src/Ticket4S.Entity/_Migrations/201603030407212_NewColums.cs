namespace Ticket4S.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewColums : DbMigration
    {
        public override void Up()
        {
            AddColumn("GatewayPayment.SavedCreditCards", "_createdAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("Purchase.PurchaseOrders", "UserRequestToSaveCreditCard", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Purchase.PurchaseOrders", "UserRequestToSaveCreditCard");
            DropColumn("GatewayPayment.SavedCreditCards", "_createdAt");
        }
    }
}
