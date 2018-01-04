namespace find_job.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "userPictur", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "userPictur");
        }
    }
}
