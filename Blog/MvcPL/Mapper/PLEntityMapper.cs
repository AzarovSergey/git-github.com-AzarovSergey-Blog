using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface.Entities;
using MvcPL.ViewModels;
using MvcPL.Models;

namespace MvcPL.Mapper
{
    public static class PLEntityMapper
    {
        public static ArticleViewModel ToPlArticle(this ArticleEntity article,UserViewModel author)
        {
            return new ArticleViewModel()
            {
                Content = article.Content,
                CreationDateTime = article.CreationDateTime,
                Author = author,
                ArticleId=article.Id,
                Title=article.Title,
                AuthorId=article.AuthorId,
            };
        }

        public static ArticleViewModel ToPlArticle(this ArticleEntity article)
        {
            return article.ToPlArticle(article.Author?.ToPlUser());
        }

        public static UserViewModel ToPlUser(this UserEntity user)
        {
            return new UserViewModel()
            {
                UserName = user.UserName,
                Ban=user.Ban,
                Login=user.Login,
                Password=user.Password,
                RoleId=user.RoleId,
                Id=user.Id,
            };
        }

        public static CommentViewModel ToPlComment(this CommentEntity comment,string authorName)
        {
            return new CommentViewModel()
            {
                ArticleId=comment.ArticleId,
                AuthorId=comment.AuthorId,
                CreationDateTime=comment.CreationDateTime,
                Id=comment.Id,
                Message=comment.Message,
                AuthorName=authorName,
            };
        }
    }
}