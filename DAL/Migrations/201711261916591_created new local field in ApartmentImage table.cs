namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creatednewlocalfieldinApartmentImagetable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApartmentImages", "Local", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApartmentImages", "Local");
        }
    }
}
