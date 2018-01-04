namespace find_job.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ad5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ADModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        adAddress = c.String(nullable: false),
                        adURL = c.String(nullable: false),
                        adImage = c.String(nullable: false),
                        adContent = c.String(nullable: false),
                        AdminAccept = c.Boolean(nullable: false),
                        userid = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.userid)
                .Index(t => t.userid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ADModels", "userid", "dbo.AspNetUsers");
            DropIndex("dbo.ADModels", new[] { "userid" });
            DropTable("dbo.ADModels");
        }
    }
}
