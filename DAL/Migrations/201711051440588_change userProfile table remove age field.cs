namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeuserProfiletableremoveagefield : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserProfiles", "BirthDay");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfiles", "BirthDay", c => c.DateTime(nullable: false));
        }
    }
}
