namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Preferences",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        ArtWorkId = c.Int(nullable: false),
                        IsLiked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ArtWorkId })
                .ForeignKey("dbo.ArtWorks", t => t.ArtWorkId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ArtWorkId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Preferences", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Preferences", "ArtWorkId", "dbo.ArtWorks");
            DropIndex("dbo.Preferences", new[] { "ArtWorkId" });
            DropIndex("dbo.Preferences", new[] { "UserId" });
            DropTable("dbo.Preferences");
        }
    }
}
