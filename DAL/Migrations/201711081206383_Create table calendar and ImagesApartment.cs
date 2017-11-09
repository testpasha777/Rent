namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatetablecalendarandImagesApartment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApartmentCalendars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Arrival = c.DateTime(nullable: false),
                        Departure = c.DateTime(nullable: false),
                        ApartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Apartments", t => t.ApartmentId, cascadeDelete: true)
                .Index(t => t.ApartmentId);
            
            CreateTable(
                "dbo.ApartmentImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApartmentId = c.Int(nullable: false),
                        PathPhoto = c.String(),
                        LinkPhoto = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Apartments", t => t.ApartmentId, cascadeDelete: true)
                .Index(t => t.ApartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApartmentImages", "ApartmentId", "dbo.Apartments");
            DropForeignKey("dbo.ApartmentCalendars", "ApartmentId", "dbo.Apartments");
            DropIndex("dbo.ApartmentImages", new[] { "ApartmentId" });
            DropIndex("dbo.ApartmentCalendars", new[] { "ApartmentId" });
            DropTable("dbo.ApartmentImages");
            DropTable("dbo.ApartmentCalendars");
        }
    }
}
