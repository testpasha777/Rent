namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedentitiTypeOFHousing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TypeOfHousings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Apartments", "TypeOfHousingId", c => c.Int(nullable: false));
            AlterColumn("dbo.ApartmentComforts", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Apartments", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Apartments", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.ApartmentImages", "PathPhoto", c => c.String(nullable: false));
            AlterColumn("dbo.ApartmentImages", "LinkPhoto", c => c.String(nullable: false));
            AlterColumn("dbo.AvailableToGuests", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Cities", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Countries", "Name", c => c.String(nullable: false));
            CreateIndex("dbo.Apartments", "TypeOfHousingId");
            AddForeignKey("dbo.Apartments", "TypeOfHousingId", "dbo.TypeOfHousings", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Apartments", "TypeOfHousingId", "dbo.TypeOfHousings");
            DropIndex("dbo.Apartments", new[] { "TypeOfHousingId" });
            AlterColumn("dbo.Countries", "Name", c => c.String());
            AlterColumn("dbo.Cities", "Name", c => c.String());
            AlterColumn("dbo.AvailableToGuests", "Name", c => c.String());
            AlterColumn("dbo.ApartmentImages", "LinkPhoto", c => c.String());
            AlterColumn("dbo.ApartmentImages", "PathPhoto", c => c.String());
            AlterColumn("dbo.Apartments", "Address", c => c.String());
            AlterColumn("dbo.Apartments", "Description", c => c.String());
            AlterColumn("dbo.ApartmentComforts", "Name", c => c.String());
            DropColumn("dbo.Apartments", "TypeOfHousingId");
            DropTable("dbo.TypeOfHousings");
        }
    }
}
