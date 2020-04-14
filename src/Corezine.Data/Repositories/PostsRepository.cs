using Corezine.Domain.Contracts;
using Corezine.Domain.Data;
using Corezine.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Corezine.Domain.Repositories
{
    public class PostsRepository : Repository<Post>, IPostsRepository
    {
        public PostsRepository(CorezineDatabaseContext context) : base(context)
        {

        }

        public override IEnumerable<Post> GetAll()
        {
            return Context.Posts.Include(p => p.User);
        }
        public IEnumerable<Post> GetAllWithCategories()
        {
            return Context.Posts.Include(p => p.User).Include(p => p.Category).ToList();
        }
    }
}
