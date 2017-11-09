namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changedbremovedevicesandrulestable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApartmentRuleApartments", "ApartmentRule_Id", "dbo.ApartmentRules");
            DropForeignKey("dbo.ApartmentRuleApartments", "Apartment_Id", "dbo.Apartments");
            DropForeignKey("dbo.ApartmentSecurityDeviceApartments", "ApartmentSecurityDevice_Id", "dbo.ApartmentSecurityDevices");
            DropForeignKey("dbo.ApartmentSecurityDeviceApartments", "Apartment_Id", "dbo.Apartments");
            DropIndex("dbo.ApartmentRuleApartments", new[] { "ApartmentRule_Id" });
            DropIndex("dbo.ApartmentRuleApartments", new[] { "Apartment_Id" });
            DropIndex("dbo.ApartmentSecurityDeviceApartments", new[] { "ApartmentSecurityDevice_Id" });
            DropIndex("dbo.ApartmentSecurityDeviceApartments", new[] { "Apartment_Id" });
            DropTable("dbo.ApartmentRules");
            DropTable("dbo.ApartmentSecurityDevices");
            DropTable("dbo.ApartmentRuleApartments");
            DropTable("dbo.ApartmentSecurityDeviceApartments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApartmentSecurityDeviceApartments",
                c => new
                    {
                        ApartmentSecurityDevice_Id = c.Int(nullable: false),
                        Apartment_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApartmentSecurityDevice_Id, t.Apartment_Id });
            
            CreateTable(
                "dbo.ApartmentRuleApartments",
                c => new
                    {
                        ApartmentRule_Id = c.Int(nullable: false),
                        Apartment_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApartmentRule_Id, t.Apartment_Id });
            
            CreateTable(
                "dbo.ApartmentSecurityDevices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApartmentRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ApartmentSecurityDeviceApartments", "Apartment_Id");
            CreateIndex("dbo.ApartmentSecurityDeviceApartments", "ApartmentSecurityDevice_Id");
            CreateIndex("dbo.ApartmentRuleApartments", "Apartment_Id");
            CreateIndex("dbo.ApartmentRuleApartments", "ApartmentRule_Id");
            AddForeignKey("dbo.ApartmentSecurityDeviceApartments", "Apartment_Id", "dbo.Apartments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApartmentSecurityDeviceApartments", "ApartmentSecurityDevice_Id", "dbo.ApartmentSecurityDevices", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApartmentRuleApartments", "Apartment_Id", "dbo.Apartments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApartmentRuleApartments", "ApartmentRule_Id", "dbo.ApartmentRules", "Id", cascadeDelete: true);
        }
    }
}
