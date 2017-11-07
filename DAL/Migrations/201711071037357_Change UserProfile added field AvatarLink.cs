namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserProfileaddedfieldAvatarLink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "AvatarLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfiles", "AvatarLink");
        }
    }
}
