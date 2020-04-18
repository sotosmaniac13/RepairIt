namespace RepairIt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RepairCategories",
                c => new
                    {
                        Cat_Id = c.Int(nullable: false, identity: true),
                        Cat_Name = c.String(),
                    })
                .PrimaryKey(t => t.Cat_Id);
            
            CreateTable(
                "dbo.CategoryTasks",
                c => new
                    {
                        CatTask_Id = c.Int(nullable: false, identity: true),
                        CatTask_Name = c.String(),
                        CatTask_Type = c.Int(nullable: false),
                        CatTask_CategoryOfThisTask_Cat_Id = c.Int(),
                    })
                .PrimaryKey(t => t.CatTask_Id)
                .ForeignKey("dbo.RepairCategories", t => t.CatTask_CategoryOfThisTask_Cat_Id)
                .Index(t => t.CatTask_CategoryOfThisTask_Cat_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategoryTasks", "CatTask_CategoryOfThisTask_Cat_Id", "dbo.RepairCategories");
            DropIndex("dbo.CategoryTasks", new[] { "CatTask_CategoryOfThisTask_Cat_Id" });
            DropTable("dbo.CategoryTasks");
            DropTable("dbo.RepairCategories");
        }
    }
}
