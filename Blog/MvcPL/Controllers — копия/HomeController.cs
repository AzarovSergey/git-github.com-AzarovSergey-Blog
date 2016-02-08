using System.Linq;
using System.Web.Mvc;
using CustomAuth.ViewModels;
//using DalToWeb.Interfacies;
using BLL.Interface.Services;
using BLL.Interface.Entities;
//using DependencyResolver;
using CustomAuth.Infrastructure;
using CustomAuth.Models;

namespace CustomAuth.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //private readonly IUserService userService = (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));

        //public HomeController(IUserRepository repository)
        //{
        //    this._repository = repository;
        //}

        private readonly IUserService userService;
        private readonly IArticleService articleService;
        private readonly IRoleService roleService;
        private readonly ITagService tagService;
        private readonly ICommentService commentService;

        public HomeController(IUserService userService, IArticleService articleService, IRoleService roleService, ITagService tagService, ICommentService commentService)
        {
            this.userService = userService;
            this.articleService = articleService;
            this.roleService = roleService;
            this.tagService = tagService;
            this.commentService = commentService;
        }
        
        public ActionResult Index()
        {
            var model = userService.GetAllUserEntities().Select(u => new UserViewModel()
            {
                Login=u.Login
            });                

            return View(model);
        }


        [HttpPost]
        //[HttpGet]
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
                // List<UserEntity> users = new List<UserEntity>(articles.);
                for (int i = 0; i < articles.Length; ++i)
                {
                    articles[i].Author = userService.GetById(articles[i].AuthorId);
                }
                TempData["articles"] = articles.ToArray();
                return RedirectToAction("ShowArticleList", "Article");
            }
            return RedirectToAction("Index", "Home");
        }

        [ChildActionOnly]
        public ActionResult Search()
        {
            return View("_Search");
        }


        public ActionResult About()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                ViewBag.AuthType = User.Identity.AuthenticationType;
            }
            ViewBag.Login = User.Identity.Name;
            ViewBag.IsAdminInRole = User.IsInRole("Admin") ?
                "You have administrator rights." : "You do not have administrator rights.";

            return View();
            //HttpContext.Profile["FirstName"] = "Вася";
            //HttpContext.Profile["LastName"] = "Иванов";
            //HttpContext.Profile.SetPropertyValue("Age",23);
            //Response.Write(HttpContext.Profile.GetPropertyValue("FirstName"));
            //Response.Write(HttpContext.Profile.GetPropertyValue("LastName"));
        }

        //[Authorize(Roles = "Admin")]
        //public ActionResult UsersEdit()
        //{
        //    var model = userService.GetAllUserEntities().Select(u => new UserViewModel
        //    {
        //        Email=u.Login,
        //        //Email = u.Email,
        //      //  CreationDate = u.CreationDate,
        //        //u.Role.Name
        //        Role = "[role name here]"
        //    });

        //    return View(model);
        //}
    }
}