using Corezine.Domain.Contracts;
using Corezine.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corezine.Site.Areas.Dashboard.ViewModels
{
    public class DashboardModel
    {
        public Int32 PostsCount { get { return Posts.Count(); } }

        public Int32 CategoriesCount { get { return Categories.Count(); } }

        public Int32 UsersCount { get { return Users.Count(); } }
        public IRepository<Category> Categories { get; }
        public IPostsRepository Posts { get; }
        public IRepository<AppUser> Users { get; }

        public DashboardModel(
            IRepository<Category> Categories, 
            IPostsRepository Posts,
            IRepository<AppUser> Users
            )
        {
            this.Categories = Categories;
            this.Posts = Posts;
            this.Users = Users;
        }
    }
}
