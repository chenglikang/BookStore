using BookStore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleManagerController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        // GET: RoleManager
        public ActionResult Index()
        {
            var list = RoleManager.Roles.ToList();
            return View(list);
        }

        // GET: RoleManager/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            // Get the list of Users in this Role
            var users = new List<ApplicationUser>();

            // Get the list of Users in this Role
            foreach (var user in UserManager.Users.ToList())
            {
                if (await UserManager.IsInRoleAsync(user.Id, role.Name))
                {
                    users.Add(user);
                }
            }

            ViewBag.Users = users;
            return View(role);
        }

        // GET: RoleManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleManager/Create
        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel roleViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var role = new ApplicationRole(roleViewModel.Name);

                    // Save the new Description property:
                    role.Description = roleViewModel.Description;
                    var roleresult = await RoleManager.CreateAsync(role);
                    if (!roleresult.Succeeded)
                    {
                        ModelState.AddModelError("", roleresult.Errors.First());
                        return View(roleViewModel);
                    }
                    return RedirectToAction("Index");
                }
                return View(roleViewModel);
            }
            catch
            {
                //TODO
                return View(roleViewModel);
            }
        }

        // GET: RoleManager/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(new RoleViewModel {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            });
        }

        // POST: RoleManager/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(RoleViewModel roleModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var role = await RoleManager.FindByIdAsync(roleModel.Id);
                    role.Name = roleModel.Name;

                    // Update the new Description property:
                    role.Description = roleModel.Description;
                    await RoleManager.UpdateAsync(role);
                    return RedirectToAction("Index");
                }
                return View(roleModel);


            }
            catch
            {
                //TODO 日志
                return View(roleModel);
            }
        }

        // GET: RoleManager/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: RoleManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    var role = await RoleManager.FindByIdAsync(id);
                    if (role == null)
                    {
                        return HttpNotFound();
                    }
                    var result = await RoleManager.DeleteAsync(role);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", result.Errors.First());
                        return View();
                    }
                    return RedirectToAction("Index");
                }
                return View();

            }
            catch
            {
                return View();
            }
        }
    }
}
