using Corezine.Domain.Contracts;
using Corezine.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corezine.Site.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize(Roles = "Administrator")]
    public class PostsController : Controller
    {
        public IPostsRepository PostsRepository { get; }
        public PostsController(IPostsRepository postsRepository)
        {
            PostsRepository = postsRepository;
        }

        public IActionResult Index()
        {
            return View(PostsRepository.GetAllWithCategories());
        }
    }
}
