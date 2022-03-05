namespace NOTEBOARD.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aaa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Content = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        UserID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        UserName = c.String(nullable: false, maxLength: 20),
                        UserType = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.UserName, unique: true);
            
            CreateTable(
                "dbo.Passwords",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 20),
                        UserID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Passwords", "UserID", "dbo.Users");
            DropForeignKey("dbo.Notes", "UserID", "dbo.Users");
            DropIndex("dbo.Passwords", new[] { "UserID" });
            DropIndex("dbo.Users", new[] { "UserName" });
            DropIndex("dbo.Notes", new[] { "UserID" });
            DropTable("dbo.Passwords");
            DropTable("dbo.Users");
            DropTable("dbo.Notes");
        }
    }
}
