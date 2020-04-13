using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corezine.Domain.Contracts;
using Corezine.Domain.Models;
using Corezine.Site.Areas.Dashboard.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Corezine.Site.Areas.Dashboard.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CategoriesController : Controller
    {
        public IRepository<Category> CategoriesRepository { get; }

        public CategoriesController(IRepository<Category> categoriesRepository)
        {
            CategoriesRepository = categoriesRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateCategoryModel());
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryModel categoryModel)
        {

            return View(categoryModel);
        }

        
    }
}