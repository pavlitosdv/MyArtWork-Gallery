namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomethingNew : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArtWorks", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ArtWorks", "ArtistId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "ArtWork_Id", "dbo.ArtWorks");
            DropForeignKey("dbo.ArtWorks", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropIndex("dbo.ArtWorks", new[] { "ArtistId" });
            DropIndex("dbo.ArtWorks", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ArtWorks", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.AspNetUsers", new[] { "ArtWork_Id" });
            //DropColumn("dbo.Preferences", "UserId");
            //RenameColumn(table: "dbo.Preferences", name: "ApplicationUser_Id1", newName: "UserId");
            CreateTable(
                "dbo.ApplicationUserArtWorks",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        ArtWork_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.ArtWork_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.ArtWorks", t => t.ArtWork_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ArtWork_Id);
            
            //AddForeignKey("dbo.Preferences", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.ArtWorks", "ArtistId");
            DropColumn("dbo.ArtWorks", "ApplicationUser_Id");
            DropColumn("dbo.ArtWorks", "ApplicationUser_Id1");
            DropColumn("dbo.AspNetUsers", "ArtWork_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ArtWork_Id", c => c.Int());
            AddColumn("dbo.ArtWorks", "ApplicationUser_Id1", c => c.String(maxLength: 128));
            AddColumn("dbo.ArtWorks", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.ArtWorks", "ArtistId", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.Preferences", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserArtWorks", "ArtWork_Id", "dbo.ArtWorks");
            DropForeignKey("dbo.ApplicationUserArtWorks", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserArtWorks", new[] { "ArtWork_Id" });
            DropIndex("dbo.ApplicationUserArtWorks", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserArtWorks");
            RenameColumn(table: "dbo.Preferences", name: "UserId", newName: "ApplicationUser_Id1");
            AddColumn("dbo.Preferences", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "ArtWork_Id");
            CreateIndex("dbo.ArtWorks", "ApplicationUser_Id1");
            CreateIndex("dbo.ArtWorks", "ApplicationUser_Id");
            CreateIndex("dbo.ArtWorks", "ArtistId");
            AddForeignKey("dbo.ArtWorks", "ApplicationUser_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "ArtWork_Id", "dbo.ArtWorks", "Id");
            AddForeignKey("dbo.ArtWorks", "ArtistId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ArtWorks", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
