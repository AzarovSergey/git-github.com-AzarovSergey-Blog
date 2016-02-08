using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;
using DAL.Interface.DTO;

namespace DAL.Mapper
{
    public static class DalEntityMapper
    {
        public static User ToOrmUser(this DalUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return new User()
            {
                UserName = user.UserName,
                Id = user.Id,
                RoleId = (int)user.RoleId,
                Password=user.Password,
                Login = user.Login,
                Ban=(bool)user.Ban,
            };
        }

        public static Article ToOrmArticle(this DalArticle article)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));
            return new Article()
            {
                AuthorId = article.AuthorId,
                Content = article.Content,
                CreationDateTime = article.CreationDateTime,
                Id = article.Id,
                Title=article.Title,
            };
        }

        public static Tag ToOrmTag(this DalTag tag)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));
            return new Tag()
            {
                Id = tag.Id,
                Value = tag.Value
            };
        }

        public static Role ToOrmRole(this DalRole role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));
            return new Role()
            {
                Id = role.Id,
                Name = role.Name,
            };
        }

        public static DalRole ToDalRole(this Role role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));
            return new DalRole()
            {
                Id = role.Id,
                Name = role.Name,
            };
        }

        public static DalUser ToDalUser(this User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return new DalUser()
            {
                Id = user.Id,
                RoleId = user.RoleId,
                UserName = user.UserName,
                Ban=user.Ban,
                Login=user.Login,
                Password=user.Password,
            };
        }

        public static DalArticle ToDalArticle(this Article article)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));
            return new DalArticle()
            {
                Id = article.Id,
                AuthorId = article.AuthorId,
                Content = article.Content,
                CreationDateTime = article.CreationDateTime,
                Title=article.Title,
            };
        }

        public static Comment ToOrmComment(this DalComment comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));
            return new Comment()
            {
                Id=comment.Id,
                ArticleId=comment.ArticleId,
                AuthorId=comment.AuthorId,
                CreationDateTime=comment.CreationDateTime,
                Message=comment.Message,
            };
        }

        public static DalComment ToDalComment(this Comment comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));
            return new DalComment()
            {
                ArticleId = comment.ArticleId,
                AuthorId = comment.AuthorId,
                CreationDateTime = comment.CreationDateTime,
                Id = comment.Id,
                Message = comment.Message,
            };
        }

        public static DalTag ToDalTag(this Tag tag)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));
            return new DalTag()
            {
                Id = tag.Id,
                Value = tag.Value,
            };
        }
    }
}
