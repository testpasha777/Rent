namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetableApartmenImages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApartmentImages", "FileName", c => c.String(nullable: false));
            AddColumn("dbo.ApartmentImages", "FolderName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApartmentImages", "FolderName");
            DropColumn("dbo.ApartmentImages", "FileName");
        }
    }
}
