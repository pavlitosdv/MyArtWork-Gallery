namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Preferences : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArtWorks", "ArtistId", "dbo.AspNetUsers");
            AddColumn("dbo.ArtWorks", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.ArtWorks", "ApplicationUser_Id1", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "ArtWork_Id", c => c.Int());
            CreateIndex("dbo.ArtWorks", "ApplicationUser_Id");
            CreateIndex("dbo.ArtWorks", "ApplicationUser_Id1");
            CreateIndex("dbo.AspNetUsers", "ArtWork_Id");
            AddForeignKey("dbo.ArtWorks", "ApplicationUser_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "ArtWork_Id", "dbo.ArtWorks", "Id");
            AddForeignKey("dbo.ArtWorks", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            DropTable("dbo.Preferences");
        }
        
        public override void Down()
        {
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
            
            DropForeignKey("dbo.ArtWorks", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "ArtWork_Id", "dbo.ArtWorks");
            DropForeignKey("dbo.ArtWorks", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "ArtWork_Id" });
            DropIndex("dbo.ArtWorks", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.ArtWorks", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AspNetUsers", "ArtWork_Id");
            DropColumn("dbo.ArtWorks", "ApplicationUser_Id1");
            DropColumn("dbo.ArtWorks", "ApplicationUser_Id");
            AddForeignKey("dbo.ArtWorks", "ArtistId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
