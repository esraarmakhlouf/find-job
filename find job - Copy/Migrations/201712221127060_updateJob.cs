namespace find_job.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateJob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "jobAddress", c => c.String());
            AddColumn("dbo.Jobs", "jobSalary", c => c.Double(nullable: false));
            AddColumn("dbo.Jobs", "jobDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "jobDate");
            DropColumn("dbo.Jobs", "jobSalary");
            DropColumn("dbo.Jobs", "jobAddress");
        }
    }
}
