using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corezine.Site.Areas.Dashboard.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Corezine.Site.Areas.Dashboard.Controllers
{
    public class CategoriesController : Controller
    {
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