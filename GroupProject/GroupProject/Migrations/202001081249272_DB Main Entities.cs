namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBMainEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Commissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserFromId = c.String(nullable: false),
                        UserToId = c.String(nullable: false),
                        DateOfCommission = c.DateTime(nullable: false),
                        Deadline = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Preferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        ArtWorkId = c.Int(nullable: false),
                        IsLiked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ArtWorks", "ArtistId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ArtWorks", "ArtistId");
            AddForeignKey("dbo.ArtWorks", "ArtistId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArtWorks", "ArtistId", "dbo.AspNetUsers");
            DropIndex("dbo.ArtWorks", new[] { "ArtistId" });
            DropColumn("dbo.ArtWorks", "ArtistId");
            DropTable("dbo.Preferences");
            DropTable("dbo.Commissions");
        }
    }
}
