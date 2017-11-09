namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create3manytomanyrelationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Apartments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Address = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaxNumberOfGuests = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        AvailableToGuestId = c.Int(nullable: false),
                        UserProfileId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AvailableToGuests", t => t.AvailableToGuestId, cascadeDelete: true)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.UserProfiles", t => t.UserProfileId)
                .Index(t => t.CityId)
                .Index(t => t.AvailableToGuestId)
                .Index(t => t.UserProfileId);
            
            CreateTable(
                "dbo.ApartmentApartmentComforts",
                c => new
                    {
                        Apartment_Id = c.Int(nullable: false),
                        ApartmentComfort_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Apartment_Id, t.ApartmentComfort_Id })
                .ForeignKey("dbo.Apartments", t => t.Apartment_Id, cascadeDelete: true)
                .ForeignKey("dbo.ApartmentComforts", t => t.ApartmentComfort_Id, cascadeDelete: true)
                .Index(t => t.Apartment_Id)
                .Index(t => t.ApartmentComfort_Id);
            
            CreateTable(
                "dbo.ApartmentRuleApartments",
                c => new
                    {
                        ApartmentRule_Id = c.Int(nullable: false),
                        Apartment_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApartmentRule_Id, t.Apartment_Id })
                .ForeignKey("dbo.ApartmentRules", t => t.ApartmentRule_Id, cascadeDelete: true)
                .ForeignKey("dbo.Apartments", t => t.Apartment_Id, cascadeDelete: true)
                .Index(t => t.ApartmentRule_Id)
                .Index(t => t.Apartment_Id);
            
            CreateTable(
                "dbo.ApartmentSecurityDeviceApartments",
                c => new
                    {
                        ApartmentSecurityDevice_Id = c.Int(nullable: false),
                        Apartment_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApartmentSecurityDevice_Id, t.Apartment_Id })
                .ForeignKey("dbo.ApartmentSecurityDevices", t => t.ApartmentSecurityDevice_Id, cascadeDelete: true)
                .ForeignKey("dbo.Apartments", t => t.Apartment_Id, cascadeDelete: true)
                .Index(t => t.ApartmentSecurityDevice_Id)
                .Index(t => t.Apartment_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Apartments", "UserProfileId", "dbo.UserProfiles");
            DropForeignKey("dbo.Apartments", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Apartments", "AvailableToGuestId", "dbo.AvailableToGuests");
            DropForeignKey("dbo.ApartmentSecurityDeviceApartments", "Apartment_Id", "dbo.Apartments");
            DropForeignKey("dbo.ApartmentSecurityDeviceApartments", "ApartmentSecurityDevice_Id", "dbo.ApartmentSecurityDevices");
            DropForeignKey("dbo.ApartmentRuleApartments", "Apartment_Id", "dbo.Apartments");
            DropForeignKey("dbo.ApartmentRuleApartments", "ApartmentRule_Id", "dbo.ApartmentRules");
            DropForeignKey("dbo.ApartmentApartmentComforts", "ApartmentComfort_Id", "dbo.ApartmentComforts");
            DropForeignKey("dbo.ApartmentApartmentComforts", "Apartment_Id", "dbo.Apartments");
            DropIndex("dbo.ApartmentSecurityDeviceApartments", new[] { "Apartment_Id" });
            DropIndex("dbo.ApartmentSecurityDeviceApartments", new[] { "ApartmentSecurityDevice_Id" });
            DropIndex("dbo.ApartmentRuleApartments", new[] { "Apartment_Id" });
            DropIndex("dbo.ApartmentRuleApartments", new[] { "ApartmentRule_Id" });
            DropIndex("dbo.ApartmentApartmentComforts", new[] { "ApartmentComfort_Id" });
            DropIndex("dbo.ApartmentApartmentComforts", new[] { "Apartment_Id" });
            DropIndex("dbo.Apartments", new[] { "UserProfileId" });
            DropIndex("dbo.Apartments", new[] { "AvailableToGuestId" });
            DropIndex("dbo.Apartments", new[] { "CityId" });
            DropTable("dbo.ApartmentSecurityDeviceApartments");
            DropTable("dbo.ApartmentRuleApartments");
            DropTable("dbo.ApartmentApartmentComforts");
            DropTable("dbo.Apartments");
        }
    }
}
