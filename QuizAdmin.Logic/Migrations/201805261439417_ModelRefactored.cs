namespace QuizAdmin.Logic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelRefactored : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        IsOK = c.Boolean(),
                        Replied = c.DateTime(),
                        Question_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Question_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Answers", "Question_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Answers", "Report_Id", c => c.Guid());
            CreateIndex("dbo.Answers", "Report_Id");
            AddForeignKey("dbo.Answers", "Report_Id", "dbo.Reports", "Id");
            DropColumn("dbo.Answers", "Question");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Answers", "Question", c => c.Int(nullable: false));
            DropForeignKey("dbo.Reports", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Reports", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Answers", "Report_Id", "dbo.Reports");
            DropIndex("dbo.Reports", new[] { "User_Id" });
            DropIndex("dbo.Reports", new[] { "Question_Id" });
            DropIndex("dbo.Answers", new[] { "Report_Id" });
            DropColumn("dbo.Answers", "Report_Id");
            DropColumn("dbo.Answers", "Question_Id");
            DropTable("dbo.Users");
            DropTable("dbo.Reports");
        }
    }
}
