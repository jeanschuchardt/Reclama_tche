namespace ReclamaTche.Migrations
{
    using Models;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using Microsoft.AspNet.Identity.EntityFramework;


    internal sealed class Configuration : DbMigrationsConfiguration<ReclamaTche.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ReclamaTche.Models.ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(r => r.Name,
             new IdentityRole
             {
                 Name = "Administrador"
             }
         );
            context.Roles.AddOrUpdate(r => r.Name,
               new IdentityRole
               {
                   Name = "Oficial"
               }
           );

            context.Roles.AddOrUpdate(r => r.Name,
               new IdentityRole
               {
                   Name = "Básico"
               }
           );

            var hasher = new PasswordHasher();
            context.Users.AddOrUpdate(p => p.Email,
                new ApplicationUser
                {
                    Email = "admin@s2b.com",
                    UserName = "admin@s2b.com",
                    PasswordHash = hasher.HashPassword("Pass@word1"),
                    LockoutEnabled = true,
                });
            context.Users.AddOrUpdate(p => p.Email,
               new ApplicationUser
               {
                   Email = "lucifer@s2b.com",
                   UserName = "lucifer@s2b.com",
                   PasswordHash = hasher.HashPassword("Pass@word1"),
                   LockoutEnabled = true,
               });
            context.Users.AddOrUpdate(p => p.Email,
               new ApplicationUser
               {
                   Email = "odin@s2b.com",
                   UserName = "odin@s2b.com",
                   PasswordHash = hasher.HashPassword("Pass@word1"),
                   LockoutEnabled = true,
                   

        });

            context.Users.AddOrUpdate(p => p.Email,
               new ApplicationUser
               {
                   Email = "orfeu@s2b.com",
                   UserName = "orfeu@s2b.com",
                   PasswordHash = hasher.HashPassword("Pass@word1"),
                   LockoutEnabled = true,
               });
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals("admin@s2b.com", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            UserManager.AddToRole(user.Id, "Administrador");
            UserManager.UpdateSecurityStamp(user.Id);
            user.LockoutEnabled = true;


            user = context.Users.Where(u => u.UserName.Equals("lucifer@s2b.com", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            UserManager.AddToRole(user.Id, "Administrador");
            UserManager.UpdateSecurityStamp(user.Id);
            user.LockoutEnabled = true;

            user = context.Users.Where(u => u.UserName.Equals("odin@s2b.com", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            UserManager.AddToRole(user.Id, "Oficial");
            UserManager.UpdateSecurityStamp(user.Id);
            user.LockoutEnabled = true;

            user = context.Users.Where(u => u.UserName.Equals("orfeu@s2b.com", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            UserManager.AddToRole(user.Id, "Básico");
            UserManager.UpdateSecurityStamp(user.Id);
            user.LockoutEnabled = true;










        }
    }
}
