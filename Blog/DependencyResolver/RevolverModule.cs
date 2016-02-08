using System.Data.Entity;
using BLL.Interface;
using BLL.Interface.Services;
using BLL.Services;
using DAL.Concrete;
using DAL.Interface.Repository;
using Ninject.Modules;
using ORM;

namespace DependencyResolver
{
    public class RevolverModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();//.InSingletonScope();

            Bind<DbContext>().To<EntityModel>().InSingletonScope();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IUserService>().To<UserService>();
            Bind<IRoleService>().To<RoleService>();
            Bind<ITagService>().To<TagService>();
            Bind<ICommentService>().To<CommentService>();

            Bind<IArticleRepository>().To<ArticleRepository>();
            Bind<IArticleService>().To<ArticleService>();
            Bind<IRoleRepository>().To<RoleRepository>();
            Bind<ITagRepository>().To<TagRepository>();
            Bind<ICommentRepository>().To<CommentRepository>();

        }
    }
}