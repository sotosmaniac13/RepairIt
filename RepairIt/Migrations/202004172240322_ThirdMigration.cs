namespace RepairIt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Cl_Id = c.Int(nullable: false, identity: true),
                        Cl_FirstName = c.String(),
                        Cl_LastName = c.String(),
                        Cl_Phone = c.String(),
                        Cl_Notes = c.String(),
                    })
                .PrimaryKey(t => t.Cl_Id);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Dev_Id = c.Int(nullable: false, identity: true),
                        Dev_Name = c.String(nullable: false),
                        Dev_Notes = c.String(),
                        Dev_Owner_Cl_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Dev_Id)
                .ForeignKey("dbo.Clients", t => t.Dev_Owner_Cl_Id)
                .Index(t => t.Dev_Owner_Cl_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Devices", "Dev_Owner_Cl_Id", "dbo.Clients");
            DropIndex("dbo.Devices", new[] { "Dev_Owner_Cl_Id" });
            DropTable("dbo.Devices");
            DropTable("dbo.Clients");
        }
    }
}
