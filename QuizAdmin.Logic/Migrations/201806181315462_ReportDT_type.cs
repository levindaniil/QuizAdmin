namespace QuizAdmin.Logic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportDT_type : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reports", "Created", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Reports", "Replied", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reports", "Replied", c => c.DateTime());
            AlterColumn("dbo.Reports", "Created", c => c.DateTime(nullable: false));
        }
    }
}
