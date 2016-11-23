using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    // 可以通过向 ApplicationUser 类添加更多属性来为用户添加配置文件数据。若要了解详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=317594。
    public class ApplicationUser : IdentityUser
    {

        //可以在Role表生成新加属性
        public int Age { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        // Use a sensible display name for views:
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }

    /// <summary>
    /// 角色类
    /// </summary>
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string name) : base(name) { }
        //可以在Role表生成新加属性
        /// <summary>
        /// 角色描述，
        /// </summary>
        public string Description { get; set; }

    }

    /// <summary>
    /// 数据库初始化器
    /// </summary>
    public class ApplicationDbInitialize : 
        DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManger =
                HttpContext.Current.GetOwinContext().
                Get<ApplicationUserManager>();

            var roleManager =
                HttpContext.Current.GetOwinContext().
                Get<ApplicationRoleManager>();

            var roleName = "admin";
            var userName = "admin@123.com";
            var userPsw = "Admin@123";

            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new ApplicationRole(roleName);
                role.Description = "系统初始化时,默认添加管理员角色";
                roleManager.Create(role);
            }

            var user = userManger.FindByName(userName);
            if (user == null)
            {
                user = new ApplicationUser();
                user.Email = userName;
                user.UserName = userName;
                userManger.Create(user,userPsw);
            }

            var roles = userManger.GetRoles(user.Id);
            if (!roles.Contains(roleName))
            {
                userManger.AddToRole(user.Id, roleName);
            }

            base.Seed(context);
        }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(
                new ApplicationDbInitialize());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}