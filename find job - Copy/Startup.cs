using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebApplication2.Models;

[assembly: OwinStartupAttribute(typeof(WebApplication2.Startup))]
namespace WebApplication2
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createDefultRolesAnadUsers();
        }
        public void createDefultRolesAnadUsers()
        {
            var roleManger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManger = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var role = new IdentityRole();
            if(!roleManger.RoleExists("Admins"))
            {
                role.Name="Admins";
                roleManger.Create(role);
                var user = new ApplicationUser();
                user.UserName = "Esraa";
                user.Email = "Esraa.makhlof999@yahoo.com";
                var check = userManger.Create(user, "akhlof999@");
                if(check.Succeeded)
                {
                    userManger.AddToRole(user.Id,"Admins");
                }
            }

        } 
    }
}
