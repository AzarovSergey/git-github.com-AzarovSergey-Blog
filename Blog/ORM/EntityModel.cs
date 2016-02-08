using System.Data.Entity;
using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ORM
{
    public partial class EntityModel : DbContext
    {
        /*
        public EntityModel() :
            base("name=EntityModel")
        { }
        //*/

        public EntityModel()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new OnceInitializer());
        }

        public  DbSet<Role> Roles { get; set; }
        public  DbSet<User> Users { get; set; }
        public  DbSet<Article> Articles { get; set; }
        public  DbSet<Tag> Tags { get; set; }
        public  DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Articles)
                .WithRequired(e => e.Author)
                .WillCascadeOnDelete(false);
        }

        private class OnceInitializer : CreateDatabaseIfNotExists<EntityModel>
        {
            protected override void Seed(EntityModel context)
            {
                base.Seed(context);

                Role roleAdmin = new Role() { Name = "Admin" };
                Role roleUser = new Role() { Name = "User" };
                User admin = new User() { Role = roleAdmin, UserName = "admin 1", Login = "admin", Password = "qwe" };

                context.Roles.Add(roleAdmin);
                context.Roles.Add(roleUser);
                context.Users.Add(admin);
            }
        }

        private class Initializer : DropCreateDatabaseAlways<EntityModel>
        {
            protected override void Seed(EntityModel context)
            {
                base.Seed(context);

                Role roleAdmin = new Role() { Name = "Admin" };
                Role roleUser = new Role() { Name = "User" };
                User admin = new User() { Role = roleAdmin, UserName = "admin 1", Login = "admin", Password = "qwe" };
                User user1 = new User() { Role = roleUser, UserName = "user 1", Login = "u1", Password = "p1" };
                User user2 = new User() { Role = roleUser, UserName = "user 2", Login = "u2", Password = "p2" };
                Tag t1 = new Tag() { Value = "TAG1" };
                Tag t2 = new Tag() { Value = "TAG2" };
                Tag t3 = new Tag() { Value = "TAG3" };
                Article a1 = new Article() { Author = admin, Content = "this is the first article", Title = "TITLE FOR ARTICLE 1" };
                Article a2 = new Article() { Author = user2, Content = "the first by user2", Title = "Title for article 2" };
                Article a3 = new Article() { Author = user2, Content = "the second by user2", Title = "Title for article 3" };
                Comment c1 = new Comment() { Article = a1, Author = user2, Message = "-1- the first comment by user2 for the first article" };
                Comment c2 = new Comment() { Article = a1, Author = admin, Message = "-2- comment by admin for the first article" };
                Comment c3 = new Comment() { Article = a2, Author = user1, Message = "-3- the first comment by user1 for the 3 article" };
                Comment c4 = new Comment() { Article = a1, Author = user2, Message = "-4- the second comment by user2 for the first article" };
                t1.Articles = new List<Article>() { a1, a2 };
                t2.Articles = new List<Article>() { a2, a3 };
                t3.Articles = new List<Article>() { a3 };

                c1.CreationDateTime = DateTime.Now.AddSeconds(1);
                c2.CreationDateTime = DateTime.Now.AddSeconds(2);
                c3.CreationDateTime = DateTime.Now.AddSeconds(3);
                c4.CreationDateTime = DateTime.Now.AddSeconds(4);

                context.Roles.Add(roleAdmin);
                context.Roles.Add(roleUser);

                context.Users.Add(admin);
                context.Users.Add(user1);
                context.Users.Add(user2);

                context.Articles.Add(a1);
                context.Articles.Add(a2);
                context.Articles.Add(a3);

                context.Tags.Add(t1);
                context.Tags.Add(t2);
                context.Tags.Add(t3);

                context.Comments.Add(c1);
                context.Comments.Add(c2);
                context.Comments.Add(c3);
                context.Comments.Add(c4);
            }
        }//Initializer

    }
}
