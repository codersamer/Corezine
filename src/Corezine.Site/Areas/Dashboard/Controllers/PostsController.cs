using Corezine.Domain.Contracts;
using Corezine.Domain.Data;
using Corezine.Domain.Models;
using Corezine.Site.Areas.Dashboard.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Corezine.Site.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize(Roles = "Administrator")]
    public class PostsController : Controller
    {
        public CorezineDatabaseContext Context { get; }
        public IPostsRepository PostsRepository { get; }
        public IRepository<Category> CategoriesRepository { get; }
        public IWebHostEnvironment HostingEnvironment { get; }
        public UserManager<AppUser> UserManager { get; }

        public PostsController(
            CorezineDatabaseContext context,
            IPostsRepository postsRepository,
            IRepository<Category> categoriesRepository,
            IWebHostEnvironment hostingEnvironment,
            UserManager<AppUser> userManager
            )
        {
            Context = context;
            PostsRepository = postsRepository;
            CategoriesRepository = categoriesRepository;
            HostingEnvironment = hostingEnvironment;
            UserManager = userManager;
        }

        public IActionResult Index()
        {
            return View(PostsRepository.GetAllWithCategories());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateUpdatePostViewModel() { Categories = CategoriesRepository.GetAll() });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUpdatePostViewModel model)
        {
            if(ModelState.IsValid)
            {
                AppUser CurrentUser = await this.UserManager.GetUserAsync(HttpContext.User);
                //Upload File
                String UploadPath = Path.Combine(HostingEnvironment.WebRootPath, "Uploads");
                if(!Directory.Exists(UploadPath)) { Directory.CreateDirectory(UploadPath); }
                String ThumbnailName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "-" + model.Thumbnail.FileName;
                String FullPath = Path.Combine(UploadPath, ThumbnailName);
                using(FileStream stream = new FileStream(FullPath, FileMode.Create, FileAccess.Write))
                { model.Thumbnail.CopyTo(stream); }
                Post post = new Post()
                {
                    CategoryId = model.CategoryId,
                    Status = model.Status,
                    Content = model.Content,
                    ThumbnailPath = ThumbnailName,
                    Title = model.Title,
                    UserId = CurrentUser.Id,
                };
                PostsRepository.Add(post);
                await PostsRepository.CommitAsync();
                return RedirectToAction("Index");
            }
            model.Categories = CategoriesRepository.GetAll();
            return View(model);
        }
    }
}
