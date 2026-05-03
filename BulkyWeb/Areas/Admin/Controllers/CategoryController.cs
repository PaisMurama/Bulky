using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBookWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork) 
        { 
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> categories = _unitOfWork.CategoryRepository.GetAll().ToList();
            return View(categories);
        }

        
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository.Add(category);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
            }
         
            return View();
        }


        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            { 
                return NotFound();
            }

            Category? categoryFromDb = _unitOfWork.CategoryRepository.GetT(c => c.Id == id);
            //Category ? categorFromDb = _db.Categoreis.Find(id);
            //Category? categoryFromDb2 = _db.Categories.FirstOrDefault(c => c.Id == id);
            //Category? categoryFromDb3 = _db.Categories.Where(c=>c.Id==id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
           
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository.Update(category);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index", "Category");
            }
              
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            Category? categoryFromDb = _unitOfWork.CategoryRepository.GetT(c => c.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? categoryFromDb = _unitOfWork.CategoryRepository.GetT(c=>c.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.CategoryRepository.Remove(categoryFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");  
          
        }


    }
}
