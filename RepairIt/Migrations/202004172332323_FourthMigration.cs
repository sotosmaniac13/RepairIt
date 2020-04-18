namespace RepairIt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourthMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Repairs",
                c => new
                    {
                        Rep_Id = c.Int(nullable: false, identity: true),
                        Rep_Subject = c.String(),
                        Rep_Description = c.String(),
                        Rep_AdditionalFixes = c.String(),
                        Rep_PendingFixes = c.String(),
                        Rep_RepairCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rep_ReceivedOn = c.DateTime(),
                        Rep_CompletedOn = c.DateTime(),
                        Rep_Category_Cat_Id = c.Int(),
                        Rep_Client_Cl_Id = c.Int(),
                        Rep_Device_Dev_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Rep_Id)
                .ForeignKey("dbo.RepairCategories", t => t.Rep_Category_Cat_Id)
                .ForeignKey("dbo.Clients", t => t.Rep_Client_Cl_Id)
                .ForeignKey("dbo.Devices", t => t.Rep_Device_Dev_Id)
                .Index(t => t.Rep_Category_Cat_Id)
                .Index(t => t.Rep_Client_Cl_Id)
                .Index(t => t.Rep_Device_Dev_Id);
            
            CreateTable(
                "dbo.CompletedTasks",
                c => new
                    {
                        Task_Id = c.Int(nullable: false, identity: true),
                        Task_BoolValue = c.Boolean(nullable: false),
                        Task_StringValue = c.String(),
                        Task_DefaultTask_CatTask_Id = c.Int(),
                        Repair_Rep_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Task_Id)
                .ForeignKey("dbo.CategoryTasks", t => t.Task_DefaultTask_CatTask_Id)
                .ForeignKey("dbo.Repairs", t => t.Repair_Rep_Id)
                .Index(t => t.Task_DefaultTask_CatTask_Id)
                .Index(t => t.Repair_Rep_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Repairs", "Rep_Device_Dev_Id", "dbo.Devices");
            DropForeignKey("dbo.CompletedTasks", "Repair_Rep_Id", "dbo.Repairs");
            DropForeignKey("dbo.CompletedTasks", "Task_DefaultTask_CatTask_Id", "dbo.CategoryTasks");
            DropForeignKey("dbo.Repairs", "Rep_Client_Cl_Id", "dbo.Clients");
            DropForeignKey("dbo.Repairs", "Rep_Category_Cat_Id", "dbo.RepairCategories");
            DropIndex("dbo.CompletedTasks", new[] { "Repair_Rep_Id" });
            DropIndex("dbo.CompletedTasks", new[] { "Task_DefaultTask_CatTask_Id" });
            DropIndex("dbo.Repairs", new[] { "Rep_Device_Dev_Id" });
            DropIndex("dbo.Repairs", new[] { "Rep_Client_Cl_Id" });
            DropIndex("dbo.Repairs", new[] { "Rep_Category_Cat_Id" });
            DropTable("dbo.CompletedTasks");
            DropTable("dbo.Repairs");
        }
    }
}
