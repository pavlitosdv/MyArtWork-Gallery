namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArtistArtWorksFinal : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationUserArtWorks", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserArtWorks", "ArtWork_Id", "dbo.ArtWorks");
            DropIndex("dbo.ApplicationUserArtWorks", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserArtWorks", new[] { "ArtWork_Id" });
            AddColumn("dbo.ArtWorks", "ArtistId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.ArtWorks", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "ArtWork_Id", c => c.Int());
            CreateIndex("dbo.ArtWorks", "ArtistId");
            CreateIndex("dbo.ArtWorks", "ApplicationUser_Id");
            CreateIndex("dbo.AspNetUsers", "ArtWork_Id");
            AddForeignKey("dbo.ArtWorks", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ArtWorks", "ArtistId", "dbo.AspNetUsers", "Id", cascadeDelete: false);
            AddForeignKey("dbo.AspNetUsers", "ArtWork_Id", "dbo.ArtWorks", "Id");
            DropTable("dbo.ApplicationUserArtWorks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApplicationUserArtWorks",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        ArtWork_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.ArtWork_Id });
            
            DropForeignKey("dbo.AspNetUsers", "ArtWork_Id", "dbo.ArtWorks");
            DropForeignKey("dbo.ArtWorks", "ArtistId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ArtWorks", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "ArtWork_Id" });
            DropIndex("dbo.ArtWorks", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ArtWorks", new[] { "ArtistId" });
            DropColumn("dbo.AspNetUsers", "ArtWork_Id");
            DropColumn("dbo.ArtWorks", "ApplicationUser_Id");
            DropColumn("dbo.ArtWorks", "ArtistId");
            CreateIndex("dbo.ApplicationUserArtWorks", "ArtWork_Id");
            CreateIndex("dbo.ApplicationUserArtWorks", "ApplicationUser_Id");
            AddForeignKey("dbo.ApplicationUserArtWorks", "ArtWork_Id", "dbo.ArtWorks", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserArtWorks", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
