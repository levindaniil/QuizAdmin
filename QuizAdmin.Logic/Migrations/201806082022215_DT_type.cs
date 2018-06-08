namespace QuizAdmin.Logic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DT_type : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Questions", "Date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Questions", "Date", c => c.DateTime(nullable: false));
        }
    }
}
