namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changestotables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArtWorks", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.Commissions", "GrandTotal", c => c.Double(nullable: false));
            DropColumn("dbo.Commissions", "Deadline");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Commissions", "Deadline", c => c.DateTime(nullable: false));
            DropColumn("dbo.Commissions", "GrandTotal");
            DropColumn("dbo.ArtWorks", "Price");
        }
    }
}
