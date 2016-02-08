using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPL.ViewModels;
using BLL.Interface.Services;
using BLL.Interface.Entities;
using MvcPL.Models;
using System.Web.Security;
using MvcPL.Providers;
using MvcPL.Mapper;
using System.Text;
using MvcPL.App_Code;

namespace MvcPL.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {
        private readonly IUserService userService;
        private readonly IArticleService articleService;
        private readonly IRoleService roleService;
        private readonly ITagService tagService;
        private readonly ICommentService commentService;
        public ArticleController(IUserService userService, IArticleService articleService, IRoleService roleService,ITagService tagService,ICommentService commentService)
        {
            this.userService = userService;
            this.articleService = articleService;
            this.roleService = roleService;
            this.tagService = tagService;
            this.commentService = commentService;
        }

        public static readonly string articleKey = "articles";
        public static readonly string commentModelKey = "commentModel";

        #region create article
        [HttpGet]
        public ActionResult Create()
        {
            UserEntity currentUser = userService.GetByLogin(Request.GetCurrentUserLogin());
            if (currentUser.Ban)
                return RedirectToAction("BanPage", "Account");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(ArticleFormViewModel articleModel)
        {
            if (ModelState.IsValid)
            {
                UserEntity currentUser = userService.GetByLogin(Request.GetCurrentUserLogin());
                if (currentUser.Ban)
                    return RedirectToAction("BanPage", "Account");
                articleService.CreateArticle(
                    new ArticleEntity() { AuthorId = currentUser.Id, Content = articleModel.Content, Title = articleModel.Title, },
                    articleModel.Tags);
                return RedirectToAction("Index", "Home");
            }
            return View(articleModel);
        }
        #endregion

        #region edit article
        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            var article= articleService.GetById(id);
            if (article == null)
                return RedirectToAction("Index", "Home");
            string tags = tagService.GetByArticleId(id).Aggregate(string.Empty, (string acc, TagEntity x) => acc + " " + x.Value);
            return View("Create", new ArticleFormViewModel() { Content = article.Content, Tags = tags, Title = article.Title });
        }

        [HttpPost]
        [Authorize]
        //id - article id
        public ActionResult Edit(int id, ArticleFormViewModel model)
        {
            UserEntity currentUser = userService.GetByLogin(Request.GetCurrentUserLogin());
            if (currentUser.Ban)
                return RedirectToAction("BanPage", "Account");
            if (User.IsInRole("Admin")||articleService.IsUserAuthor(id, currentUser.Id))
            {
                articleService.Edit(new ArticleEntity() { Content = model.Content, Id = id,Title=model.Title }, model.Tags);
                return RedirectToAction("Show", "Article", new { id = id });
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region show articles
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Show(int id)
        {
            ArticleEntity articleEntity = articleService.GetById(id);
            if (articleEntity == null)
                return RedirectToAction("Index", "Home");
            ArticleViewModel article = articleEntity.ToPlArticle();

            var author =userService.GetById(articleEntity.AuthorId);
            if (author!=null)
                article.Author = author.ToPlUser();

            article.Comments = commentService
                .GetByArticleId(id)?
                .Select(x => x.ToPlComment(userService.GetById(x.AuthorId).UserName));
            article.ArticleId = id;
            article.Tags = tagService.GetByArticleId(id).Select(x=>x.Value);
            var user = Request.GetCurrentUserLogin();
            if (user != null)
                TempData["user"] = userService.GetByLogin(user).ToPlUser();
            return View(article);
        }

        [HttpGet]
        [AllowAnonymous]
        //id is userId
        public ActionResult ShowUserArticles(int id)
        {
            UserViewModel user = userService.GetById(id)?.ToPlUser();
            ArticleViewModel[] articles = articleService.GetByAuthorId(id)?.Select(x => x.ToPlArticle()).ToArray();
            if (articles == null || user == null)
                return RedirectToAction("Index", "Home");
            for (int i = 0; i < articles.Length; ++i)
            {
                articles[i].Author = user;
                articles[i].Tags = tagService.GetByArticleId(articles[i].ArticleId).Select(x => x.Value);
            }
            if (User.Identity.IsAuthenticated)
                TempData["user"] = userService.GetByLogin(Request.GetCurrentUserLogin()).ToPlUser();
            return View("ArticleList", articles);
        }

        [AllowAnonymous]
        /// <summary>
        /// returns view with articles get from TempData.
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowArticleList()
        {
            if (!TempData.ContainsKey(articleKey))
                return RedirectToAction("Index", "Home");
            IEnumerable<ArticleEntity> articles = TempData.Peek(articleKey) as IEnumerable<ArticleEntity>;
            if (articles == null)
                return RedirectToAction("Index", "Home");
            TempData.Remove(articleKey);
            return View("ArticleList", articles.Select(x => x.ToPlArticle()));
        }
        #endregion

        #region remove article
        public ActionResult Remove(int id)
        {
            articleService.Remove(id);
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region comments
        [HttpPost]
        ///id is articleId
        public ActionResult AddComment(int id,CommentFormViewModel commentModel)
        {
            UserEntity currentUser = userService.GetByLogin(Request.GetCurrentUserLogin());
            if (currentUser.Ban)
                return RedirectToAction("BanPage", "Account");

            if (ModelState.IsValid)
            {
                if (commentModel.CommentId == 0)
                {   //add new comment
                    commentService.Create(new CommentEntity()
                    {
                        ArticleId = id,
                        AuthorId = userService.GetByLogin(Request.GetCurrentUserLogin()).Id,
                        Message = commentModel.Comment
                    });
                }
                else//edit comment
                {
                    commentService.Edit(commentModel.CommentId, commentModel.Comment);
                }
            }
            return Redirect(@"~/Article/Show/"+id.ToString());
        }

       
        
        [HttpPost]
        //id is comment id
        public ActionResult EditComment(int id,CommentFormViewModel model)
        {
            UserEntity currentUser = userService.GetByLogin(Request.GetCurrentUserLogin());
            if (currentUser.Ban)
                return RedirectToAction("BanPage", "Account");

            if (ModelState.IsValid)
            {
                if (commentService.GetById(id).AuthorId == currentUser.Id)
                {
                    commentService.Edit(id, model.Comment);
                    return View();
                }
            }
            //TODO return page where we now
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        //id is comment id
        public ActionResult EditComment(int id)
        {
            CommentEntity comment = commentService.GetById(id);
            if (comment!=null)
            {
                TempData[commentModelKey] = new CommentFormViewModel() { Comment = comment.Message, CommentId = id, ArticleId = comment.ArticleId };
                TempData["user"] = userService.GetByLogin(Request.GetCurrentUserLogin()).ToPlUser();
                return Redirect(@"~/Article/Show/" + comment.ArticleId.ToString());
            }
            return RedirectToAction("Index", "Home");
        }

        
        //id is comment id
        [HttpGet]
        public ActionResult RemoveComment(int id)
        {
            var comment = commentService.GetById(id);
            if (comment != null)
            {
                commentService.Remove(id);
                return RedirectToAction("Show", "Article", new { id = comment.ArticleId });
            }
            return RedirectToAction("Index", "Home");
        }
        
        [AllowAnonymous]
        public ActionResult GetComments(int id)
        {
            if (User.Identity.IsAuthenticated)
                TempData["user"] = userService.GetByLogin(Request.GetCurrentUserLogin()).ToPlUser();
            IEnumerable<CommentViewModel> model = commentService.GetByArticleId(id).ToArray().Select(comment => comment.ToPlComment(userService.GetById(comment.AuthorId).UserName));
            return View("_CommentsList",model);
        }
        #endregion




    }
}