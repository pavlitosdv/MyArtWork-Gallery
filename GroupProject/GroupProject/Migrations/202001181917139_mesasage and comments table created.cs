namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mesasageandcommentstablecreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentBody = c.String(maxLength: 255),
                        UserId = c.String(),
                        ArtWorkId = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArtWorks", t => t.ArtWorkId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ArtWorkId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Messages = c.String(nullable: false),
                        UserTo = c.String(nullable: false, maxLength: 128),
                        UserFrom = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserFrom, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserTo)
                .Index(t => t.UserTo)
                .Index(t => t.UserFrom);
            
            AddColumn("dbo.AspNetUsers", "ProfilePicture", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "UserTo", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "UserFrom", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "ArtWorkId", "dbo.ArtWorks");
            DropIndex("dbo.Messages", new[] { "UserFrom" });
            DropIndex("dbo.Messages", new[] { "UserTo" });
            DropIndex("dbo.Comments", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Comments", new[] { "ArtWorkId" });
            DropColumn("dbo.AspNetUsers", "ProfilePicture");
            DropTable("dbo.Messages");
            DropTable("dbo.Comments");
        }
    }
}
