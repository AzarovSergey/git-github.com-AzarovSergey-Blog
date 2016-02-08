using System;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using MvcPL.Infrastructure;
using MvcPL.Providers;
using MvcPL.ViewModels;
using MvcPL.Mapper;
using BLL.Interface.Services;
using MvcPL.Models;
using System.Collections.Generic;
using System.Web;

namespace MvcPL.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IArticleService articleService;
        private readonly IRoleService roleService;
        
        public AccountController(IUserService userService, IArticleService articleService, IRoleService roleService)
        {
            this.userService = userService;
            this.articleService = articleService;
            this.roleService = roleService;
        }

        #region Login and Logout
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogOnViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(viewModel.Login, viewModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Login, viewModel.RememberMe);
                    TempData["user"] = userService.GetByLogin(viewModel.Login).ToPlUser();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password.");
                }
            }
            return View(viewModel);
        }
        
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            TempData.Remove("user");
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Registration
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            var anyUser = userService.GetAllUserEntities().Any(u => u.Login==viewModel.Login);

            if (anyUser)
            {
                ModelState.AddModelError("", "User with this address already registered.");
                return View(viewModel);
            }

            if (ModelState.IsValid)
            {
                var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                    .CreateUser(viewModel.Login, viewModel.Password,viewModel.Name);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Login, false);
                    TempData["user"] = userService.GetByLogin(viewModel.Login).ToPlUser();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Error registration.");
                }
            }
            return View(viewModel);
        }
        #endregion

        #region ban/unban
        [Authorize(Roles = "Admin")]
        public ActionResult Ban(int id)
        {
            userService.SetBan(id, true);
            return RedirectToAction("AdminPage");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Unban(int id)
        {
            userService.SetBan(id, false);
            return RedirectToAction("AdminPage");
        }
        #endregion

        #region other
        [Authorize(Roles = "Admin")]
        public ActionResult AdminPage()
        {
            IEnumerable<UserViewModel> users = userService.GetAllUserEntities().Select(user => user.ToPlUser());
            return View(users);
        }

        [AllowAnonymous]
        public ActionResult BanPage()
        {
            return View();
        }
        #endregion
    }
}