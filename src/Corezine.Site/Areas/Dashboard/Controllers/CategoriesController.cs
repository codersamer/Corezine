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
    [Area("Dashboard")]
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
            return View(CategoriesRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateUpdateCategoryModel());
        }

        [HttpPost]
        public IActionResult Create(CreateUpdateCategoryModel categoryModel)
        {
            if(ModelState.IsValid)
            {
                CategoriesRepository.Add(new Category() { Name = categoryModel.Name });
                CategoriesRepository.Commit();
                return RedirectToAction("Index");
            }
            return View(categoryModel);
        }

        [HttpGet]
        public IActionResult Edit(Int32 id)
        {
            Category category = CategoriesRepository.Get(id);
            if(category != null)
            {
                return View("Create", new CreateUpdateCategoryModel() { CategoryId = category.Id, Name = category.Name });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(CreateUpdateCategoryModel categoryModel)
        {
            if (!ModelState.IsValid) { return View("Create", categoryModel); }

            Category category = CategoriesRepository.Get(categoryModel.CategoryId);
            if(category != null)
            {
                category.Name = categoryModel.Name;
                CategoriesRepository.Commit();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(Int32 id)
        {
            Category category = CategoriesRepository.Get(id);
            if(category != null)
            {
                CategoriesRepository.Remove(category);
                CategoriesRepository.Commit();
            }
            return RedirectToAction("Index");
        }

        
    }
}