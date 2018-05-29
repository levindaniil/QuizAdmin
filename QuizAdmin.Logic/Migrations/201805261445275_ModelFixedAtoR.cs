namespace QuizAdmin.Logic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelFixedAtoR : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answers", "Report_Id", "dbo.Reports");
            DropIndex("dbo.Answers", new[] { "Report_Id" });
            DropColumn("dbo.Answers", "Report_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Answers", "Report_Id", c => c.Guid());
            CreateIndex("dbo.Answers", "Report_Id");
            AddForeignKey("dbo.Answers", "Report_Id", "dbo.Reports", "Id");
        }
    }
}
