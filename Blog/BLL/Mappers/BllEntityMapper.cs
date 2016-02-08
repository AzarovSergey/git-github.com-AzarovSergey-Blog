using BLL.Interface.Entities;
using DAL.Interface.DTO;
using System;

namespace BLL.Mappers
{
    public static class BllEntityMapper
    {
        public static DalUser ToDalUser(this UserEntity userEntity)
        {
            if (userEntity == null)
                throw new ArgumentNullException(nameof(userEntity));
            return new DalUser()
            {
                Id = userEntity.Id,
                UserName = userEntity.UserName,
                RoleId = userEntity.RoleId,
                Login = userEntity.Login,
                Password = userEntity.Password,
                Ban = userEntity.Ban
            };
        }

        public static UserEntity ToBllUser(this DalUser dalUser)
        {
            if (dalUser == null)
                throw new ArgumentNullException(nameof(dalUser));
            return new UserEntity()
            {
                Id = dalUser.Id,
                UserName = dalUser.UserName,
                RoleId = (int)dalUser.RoleId,
                Login = dalUser.Login,
                Password = dalUser.Password,
                Ban=(bool)dalUser.Ban,
            };
        }

        public static DalArticle ToDalArticle(this ArticleEntity article)
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


        public static ArticleEntity ToBllArticle(this DalArticle article)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));
            return new ArticleEntity()
            {
                Id = article.Id,
                AuthorId = article.AuthorId,
                Content = article.Content,
                CreationDateTime = article.CreationDateTime,
                Title=article.Title,
            };
        }

        public static RoleEntity ToBllRole(this DalRole role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));
            return new RoleEntity()
            {
                Id = role.Id,
                Name=role.Name,
            };
        }

        public static DalTag ToDalTag(this TagEntity tag)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));
            return new DalTag()
            {
                Id = tag.Id,
                Value = tag.Value,
            };
        }

        public static TagEntity ToBllTag(this DalTag tag)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));
            return new TagEntity()
            {
                Id=tag.Id,
                Value=tag.Value,
            };
        }

        public static DalComment ToDalComment(this CommentEntity comment)
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

        public static CommentEntity ToBllComment(this DalComment comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));
            return new CommentEntity()
            {
                ArticleId = comment.ArticleId,
                AuthorId = comment.AuthorId,
                CreationDateTime = comment.CreationDateTime,
                Id = comment.Id,
                Message = comment.Message,
            };
        }
    }
}
