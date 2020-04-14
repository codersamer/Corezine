using Corezine.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corezine.Domain.Contracts
{
    public interface IPostsRepository : IRepository<Post>
    {
        IEnumerable<Post> GetAllWithCategories();
    }
}
