namespace QuizAdmin.Logic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionRefactored : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Answers", "Question_Id", c => c.Int());
            CreateIndex("dbo.Answers", "Question_Id");
            AddForeignKey("dbo.Answers", "Question_Id", "dbo.Questions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "Question_Id", "dbo.Questions");
            DropIndex("dbo.Answers", new[] { "Question_Id" });
            AlterColumn("dbo.Answers", "Question_Id", c => c.Int(nullable: false));
        }
    }
}
