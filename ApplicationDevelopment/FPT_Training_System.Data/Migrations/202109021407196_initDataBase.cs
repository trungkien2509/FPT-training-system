namespace FPT_Training_System.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fullname = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50, unicode: false),
                        Password = c.String(maxLength: 50, unicode: false),
                        Age = c.Int(),
                        Dob = c.DateTime(storeType: "date"),
                        Education = c.String(maxLength: 50),
                        MainPpLanguage = c.String(maxLength: 50, unicode: false),
                        ToeicScore = c.Int(),
                        ExperienceDetails = c.String(maxLength: 100),
                        Department = c.String(maxLength: 50),
                        Location = c.String(maxLength: 60),
                        Contact = c.String(maxLength: 60),
                        isActive = c.String(maxLength: 15, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AssignedCourse",
                c => new
                    {
                        AssignedId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        CourseId = c.Int(),
                    })
                .PrimaryKey(t => t.AssignedId)
                .ForeignKey("dbo.Course", t => t.CourseId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseName = c.String(maxLength: 60),
                        CourseCateId = c.Int(),
                        CourseDescription = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.CourseCategory", t => t.CourseCateId)
                .Index(t => t.CourseCateId);
            
            CreateTable(
                "dbo.CourseCategory",
                c => new
                    {
                        CourseCateId = c.Int(nullable: false, identity: true),
                        CourseCateName = c.String(maxLength: 50),
                        CourseCateDescription = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.CourseCateId);
            
            
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AssignedCourse", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Course", "CourseCateId", "dbo.CourseCategory");
            DropForeignKey("dbo.AssignedCourse", "CourseId", "dbo.Course");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Course", new[] { "CourseCateId" });
            DropIndex("dbo.AssignedCourse", new[] { "CourseId" });
            DropIndex("dbo.AssignedCourse", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropTable("dbo.CourseCategory");
            DropTable("dbo.Course");
            DropTable("dbo.AssignedCourse");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
