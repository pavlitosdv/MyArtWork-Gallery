namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changestomodelsandaddedcontrollers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArtWorks", "Thumbnail", c => c.String());
            AddColumn("dbo.Preferences", "IsLiked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Preferences", "IsLiked");
            DropColumn("dbo.ArtWorks", "Thumbnail");
        }
    }
}
