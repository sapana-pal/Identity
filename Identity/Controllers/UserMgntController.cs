using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Controllers
{
   // [Authorize(Roles = "Admin,SuperAdmin,Manager")]
    [Authorize]

    public class UserMgntController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<AppUser> userManager;
        private IPasswordHasher<AppUser> passwordHasher;
        private readonly AppIdentityDbContext _IdentityDbContext;
        public UserMgntController(RoleManager<IdentityRole> roleMgr, UserManager<AppUser> userMrg, IPasswordHasher<AppUser> passwordHash, AppIdentityDbContext IdentityDbContext)
        {
            roleManager = roleMgr;
            userManager = userMrg;
            passwordHasher = passwordHash;
            _IdentityDbContext = IdentityDbContext;
        }

        [Authorize(Roles = "Admin,SuperAdmin,Users,Manager")]
        public IActionResult Index(int id)
        {
            ViewData["HasPermissionOfAddUser"] = User.IsInRole("SuperAdmin");
            ViewData["HasPermissionOfUpdateUser"] = User.IsInRole("SuperAdmin,Admin");
            ViewData["HasPermissionOfListUser"] = User.IsInRole("SuperAdmin,Admin,Manager");
            ViewData["HasPermissionOfViewUser"] = User.IsInRole("Users,SuperAdmin");
            //ViewData["HasPermissionOfDeleteUser"] = User.IsInRole("SuperAdmin,Manager");

            //ViewBag.Userid= userManager.GetUserId(HttpContext.User);

            //var userid =userManager.GetUserId(HttpContext.User);
            //if(userid==null)
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            //else
            //{
            //    AppUser user=userManager.FindByIdAsync(userid).Result;
            //    return View(user);
            //}
            var users = userManager.Users;
            if (User.IsInRole("Users"))
            {
                var userid = userManager.GetUserId(HttpContext.User);
                users = users.Where(x => x.Id == userid);
                //if (users != null)
                //    return View(users);
                //else
                //    return RedirectToAction("View" +userid);

                //return RedirectToRoute("default", new { Controller = "UserMgnt", action = "View" });

                //if(userid==null)
                //{
                //    return RedirectToAction("View");
                //}
                //else
                //{
                //    AppUser user=userManager.FindByIdAsync(userid).Result;
                //    return View(user);
                //}


                if(users !=null && users.Any())
                {
                    //return RedirectToAction("View/"+users.FirstOrDefault().Id);
                    //return RedirectToAction("View"+userid);
                    return RedirectToAction("View" ,new {users.FirstOrDefault().Id});
                }
                else
                {
                    return View(users);

                }
                //string Users = form["userid"];
                //if(Users == "Users")
                //{
                //    return RedirectToAction("Index", new { Controller = "Home", action = "Index", userid = userid });

                //}
                //else
                //{
                //    return RedirectToAction("Index");
                //}
                //if (userid == userid && Command == "Back")
                //{
                //    return RedirectToAction("Index", new { Controller = "Home", action = "Index" });
                //    //return RedirectToAction(nameof(Index));
                //    //var url = Url.RouteUrl("Index", new { controller = "[Home]", action = "Index" });
                //    //return Redirect(url);   
                //}
                //else
                //{
                //    return RedirectToAction("Index");
                //}

                //if(User.IsInRole("Users") && Command == "Back")
                //{
                //    if(userid== userid)
                //    {
                //        Response.Redirect("Home/Index");
                //    }
                //}
                //else
                //{
                //    if(User.IsInRole("Users"))
                //    {
                //        if(userid!= userid)
                //        {
                //            Response.Redirect("Index");
                //        }

                //    }
                //}
            }
            return View(users);
            //return RedirectToAction("View");
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    UserName = user.Name,
                    Email = user.Email,
                    //TwoFactorEnabled = true
                };

                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);

                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }

        [Authorize(Roles = "SuperAdmin,Users")]
        public async Task<IActionResult> Update(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Users")]
        public async Task<IActionResult> Update(string id, string email, string password)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(email))
                    user.Email = email;
                else
                    ModelState.AddModelError("", "Email cannot be empty");

                if (!string.IsNullOrEmpty(password))
                    user.PasswordHash = passwordHasher.HashPassword(user, password);
                else
                    ModelState.AddModelError("", "Password cannot be empty");

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(user);
        }


        [Authorize(Roles = "SuperAdmin,Users")]

        public async Task<IActionResult> View(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Users")]
        public async Task<IActionResult> View(string id, string email, string password)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(email))
                    user.Email = email;
                else
                    ModelState.AddModelError("", "Email cannot be empty");

                if (!string.IsNullOrEmpty(password))
                    user.PasswordHash = passwordHasher.HashPassword(user, password);
                else
                    ModelState.AddModelError("", "Password cannot be empty");

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(user);
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Manager")]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View("Index", userManager.Users);
        }
    }
}

