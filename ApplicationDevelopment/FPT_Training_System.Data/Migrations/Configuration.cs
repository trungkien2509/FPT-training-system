using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using FPT_Training_System.Data.Models;

namespace FPT_Training_System.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<TrainingProgramManagerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TrainingProgramManagerDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            //context.Database.Initialize(true);

            SeedUser(context);
            SeedRole(context);
            SeedUserRole(context);
            SeedCourseCategory(context);
            SeedCourse(context);

            base.Seed(context);
        }

        private void SeedUser(TrainingProgramManagerDbContext context)
        {

            var seedUsers = new List<AspNetUser>
            {
                new AspNetUser {Fullname = "Lê Trung Kiên", Email = "Kien.Admin@fpt.vn", Password = "0909", Contact = "0981817390",Age =25,Education="Bachelor",ToeicScore=900,ExperienceDetails="5 years",Department="D6",Location="HCM"},
                new AspNetUser {Fullname = "Nguyễn Can", Email = "Can.Staff@fpt.com", Password = "0909", Contact = "0981817391",Age =26,Education="Bachelor",ToeicScore=900,ExperienceDetails="5 years",Department="D6",Location="HCM"},
                new AspNetUser {Fullname = "Will Smith", Email = "Smith.Trainer@fpt.com", Password = "0909", Contact = "0981817394",Age =26,Education="Bachelor",MainPpLanguage ="Java",ToeicScore=900,ExperienceDetails="5 years",Department="D6",Location="HCM"},
                new AspNetUser {Fullname = "David Villa", Email = "Villa.Trainee@fpt.com", Password = "0909", Contact = "0981817392",Age =18,Department="D6",Location="HCM"}
            };

            var emails = seedUsers.Select(p => p.Email);

            var existingUsers = context.AspNetUsers.Where(p => emails.Contains(p.Email)).ToList();

            var insertedUsers = seedUsers.Where(p => !existingUsers.Any(q => q.Email == p.Email)).ToList();
            var needSaveChanges = false;
            foreach (var insertedUser in insertedUsers)
            {
                context.AspNetUsers.Add(insertedUser);
                needSaveChanges = true;
            }

            if (needSaveChanges)
            {
                context.SaveChanges();
            }
        }
        private void SeedRole(TrainingProgramManagerDbContext context)
        {

            var seedRole = new List<AspNetRole>
            {
                new AspNetRole {Name = "Admin"},
                new AspNetRole {Name = "Training Staff"},
                new AspNetRole {Name = "Trainer"},
                new AspNetRole {Name = "Trainee"},
            };

            var namerole = seedRole.Select(p => p.Name);

            var existingNameRoles = context.AspNetRoles.Where(p => namerole.Contains(p.Name)).ToList();

            var insertedNameRoles = seedRole.Where(p => !existingNameRoles.Any(q => q.Name == p.Name)).ToList();
            var needSaveChanges = false;
            foreach (var insertedNameRole in insertedNameRoles)
            {
                context.AspNetRoles.Add(insertedNameRole);
                needSaveChanges = true;
            }

            if (needSaveChanges)
            {
                context.SaveChanges();
            }
        }
        private void SeedUserRole(TrainingProgramManagerDbContext context)
        {

            var seedUserRole = new List<AspNetUserRole>
            {
                new AspNetUserRole {RoleId = 1, UserId = 1},
                new AspNetUserRole {RoleId = 2, UserId = 2},
                new AspNetUserRole {RoleId = 3, UserId = 3},
                new AspNetUserRole {RoleId = 4, UserId = 4},
            };

            //var userrole = seedUserRole.Select(p => new { p.RoleId, p.UserId });

            var roleId = seedUserRole.Select(p => p.RoleId);
            var userId = seedUserRole.Select(p => p.UserId);

            //var existingUserRoles = context.AspNetUserRoles.Where(p => userrole.Contains(new { p.RoleId, p.UserId })).ToList();

            var existingUserRoles = context.AspNetUserRoles.Where(p => roleId.Contains(p.RoleId) && userId.Contains(p.UserId)).ToList();

            var insertedUserRoles = seedUserRole.Where(p => !existingUserRoles.Any(q => q.RoleId == p.RoleId && q.UserId == p.UserId)).ToList();
            var needSaveChanges = false;
            foreach (var insertedUserRole in insertedUserRoles)
            {
                context.AspNetUserRoles.Add(insertedUserRole);
                needSaveChanges = true;
            }

            if (needSaveChanges)
            {
                context.SaveChanges();
            }
        }
        private void SeedCourseCategory(TrainingProgramManagerDbContext context)
        {

            var seedCourseCate = new List<CourseCategory>
            {
                new CourseCategory {CourseCateName = "Computing", CourseCateDescription = "Computing description"},
                new CourseCategory {CourseCateName = "Business ", CourseCateDescription = "Business  Description"},
            };

            var nameCourseCate = seedCourseCate.Select(p => p.CourseCateName);

            var existingCourseCate = context.CourseCategories.Where(p => nameCourseCate.Contains(p.CourseCateName)).ToList();

            var insertedCourseCates = seedCourseCate.Where(p => !existingCourseCate.Any(q => q.CourseCateName == p.CourseCateName)).ToList();

            var needSaveChanges = false;
            foreach (var insertedCourseCate in insertedCourseCates)
            {
                context.CourseCategories.Add(insertedCourseCate);
                needSaveChanges = true;
            }

            if (needSaveChanges)
            {
                context.SaveChanges();
            }
        }
        private void SeedCourse(TrainingProgramManagerDbContext context)
        {

            var seedCourse = new List<Course>
            {
                new Course {CourseName = "Application development", CourseCateId=1, CourseDescription = "Application development description"},
                new Course {CourseName = "Business Intelligence", CourseCateId=2, CourseDescription = "Business Intelligence Description"},
            };

            var nameCourse = seedCourse.Select(p => p.CourseName);

            var existingCourse = context.Courses.Where(p => nameCourse.Contains(p.CourseName)).ToList();

            var insertedCourses = seedCourse.Where(p => !existingCourse.Any(q => q.CourseName == p.CourseName)).ToList();

            var needSaveChanges = false;
            foreach (var insertedCourse in insertedCourses)
            {
                context.Courses.Add(insertedCourse);
                needSaveChanges = true;
            }

            if (needSaveChanges)
            {
                context.SaveChanges();
            }
        }
    }
}
