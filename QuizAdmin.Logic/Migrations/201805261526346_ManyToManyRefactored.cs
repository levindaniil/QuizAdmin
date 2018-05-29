namespace QuizAdmin.Logic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManyToManyRefactored : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReportAnswers",
                c => new
                    {
                        Report_Id = c.Guid(nullable: false),
                        Answer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Report_Id, t.Answer_Id })
                .ForeignKey("dbo.Reports", t => t.Report_Id, cascadeDelete: true)
                .ForeignKey("dbo.Answers", t => t.Answer_Id, cascadeDelete: true)
                .Index(t => t.Report_Id)
                .Index(t => t.Answer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReportAnswers", "Answer_Id", "dbo.Answers");
            DropForeignKey("dbo.ReportAnswers", "Report_Id", "dbo.Reports");
            DropIndex("dbo.ReportAnswers", new[] { "Answer_Id" });
            DropIndex("dbo.ReportAnswers", new[] { "Report_Id" });
            DropTable("dbo.ReportAnswers");
        }
    }
}
