namespace PizzaWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcurrentVouchertoorder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CurrentVoucher", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "CurrentVoucher");
        }
    }
}
