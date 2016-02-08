using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Services;
using BLL.Interface.Entities;
using MvcPL.Models;
using MvcPL.ViewModels;
using MvcPL.Mapper;
using MvcPL.Infrastructure;
using MvcPL.Providers;
using MvcPL.App_Code;

namespace MvcPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        private readonly IArticleService articleService;
        private readonly IRoleService roleService;
        private readonly ITagService tagService;
        private readonly ICommentService commentService;

        public HomeController(IUserService userService,IArticleService articleService,IRoleService roleService,ITagService tagService,ICommentService commentService)
        {
            this.userService = userService;
            this.articleService = articleService;
            this.roleService = roleService;
            this.tagService = tagService;
            this.commentService = commentService;
        }
        
        // GET: Home
        [AllowAnonymous]
        public ActionResult Index()
        {
            ArticleViewModel[] articles = articleService.GetAll().Select(x => x.ToPlArticle()).ToArray();
            for (int i = 0; i < articles.Length; i++)
            {
                articles[i].Tags= tagService.GetByArticleId(articles[i].ArticleId).Select(tag => tag.Value);
                articles[i].Author = userService.GetById(articles[i].AuthorId).ToPlUser();
            }
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = userService.GetByLogin(Request.GetCurrentUserLogin())?.ToPlUser();
                if (currentUser!=null)
                    TempData["user"] = currentUser;
            }
            TempData[ArticleController.articleKey] = articles;
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Search(SearchViewModel searchModel)
        {
            if (ModelState.IsValid)
            {
                searchModel.SearchString = searchModel.SearchString.Trim();
                ArticleEntity[] articles;
                if (searchModel.IsSearchByTag)
                {
                    articles = articleService.GetByTag(searchModel.SearchString).ToArray();
                }
                else
                {
                    articles = articleService.SearchInContent(searchModel.SearchString).ToArray();
                }
                for(int i=0;i<articles.Length;++i)
                {
                    articles[i].Author = userService.GetById(articles[i].AuthorId);
                }
                TempData["articles"] = articles;
                return RedirectToAction("ShowArticleList", "Article");
            }
            return RedirectToAction("Index", "Home");
        }

        [ChildActionOnly]
        public ActionResult Search()
        {
            return View("_Search");
        }
        
        
    }
}
