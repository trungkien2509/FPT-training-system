using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApplicationDevelopment.Entities
{
    public class DataSeeding : DropCreateDatabaseAlways<TrainingProgramManagerDbContext>
    {
        protected override void Seed(TrainingProgramManagerDbContext context)
        {
            //IList<AspNetUser> defaultAspNetUser = new List<AspNetUser>();

            //defaultAspNetUser.Add(new AspNetUser() { Fullname = "Đạt", Age=19});
            //defaultAspNetUser.Add(new AspNetUser() { Fullname = "User 2"});
            //defaultAspNetUser.Add(new AspNetUser() { Fullname = "User 3"});

            //context.AspNetUsers.AddRange(defaultAspNetUser);

            //base.Seed(context);
            var users = new List<AspNetUser>
            {
                new AspNetUser{Fullname="Nguyễn Hoàng Duy", Email="abc", Contact="088888"},
                new AspNetUser{Fullname="Lê Trung Kiên", Email="abc", Contact="099999"},
            };
            users.ForEach(s => context.AspNetUsers.Add(s));
            context.SaveChanges();
        }
    }
}