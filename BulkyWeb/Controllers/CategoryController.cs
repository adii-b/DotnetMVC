using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db) // Dependency Injection
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name cannot match Display Order"); // Note that the key has to match the asp-validation-for helper name so that it can display error messages
            }

            //if (obj.Name != null && obj.Name.ToLower() == "test") // case insensitive
            //{
            //    ModelState.AddModelError("", "Name value cannot be test"); // If key isn't mentioned the error is displayed in the tag containing asp-validation-summary
            //}
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index", "Category"); // Redirects to Index Action after a category is added
            }
            else
            {
                TempData["error"] = "Failed to create category";
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.FirstOrDefault(c => c.Id == id); // Works only on primary key use firstOrDefault if key is not primary
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name cannot match Display Order"); // Note that the key has to match the asp-validation-for helper name so that it can display error messages
            }

            //if (obj.Name != null && obj.Name.ToLower() == "test") // case insensitive
            //{
            //    ModelState.AddModelError("", "Name value cannot be test"); // If key isn't mentioned the error is displayed in the tag containing asp-validation-summary
            //}
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index", "Category"); // Redirects to Index Action after a category is added
            }
            else
            {
                TempData["error"] = "Failed to update category";
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.FirstOrDefault(c => c.Id == id); // Works only on primary key use firstOrDefault if key is not primary
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")] // Add ActionName if your method name is something else which you don't want to be action name
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                TempData["error"] = "Failed to delete category";
                return RedirectToAction("Index", "Category");    
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
