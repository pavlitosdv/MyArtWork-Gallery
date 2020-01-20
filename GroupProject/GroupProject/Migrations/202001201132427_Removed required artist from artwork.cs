namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removedrequiredartistfromartwork : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArtWorks", "Artist_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ArtWorks", new[] { "Artist_Id" });
            AlterColumn("dbo.ArtWorks", "Artist_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ArtWorks", "Artist_Id");
            AddForeignKey("dbo.ArtWorks", "Artist_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArtWorks", "Artist_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ArtWorks", new[] { "Artist_Id" });
            AlterColumn("dbo.ArtWorks", "Artist_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ArtWorks", "Artist_Id");
            AddForeignKey("dbo.ArtWorks", "Artist_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
