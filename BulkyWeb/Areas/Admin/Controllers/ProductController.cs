using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> products = _unitOfWork.ProductRepository.GetAll(includeProperties: "Category").ToList();
            return View(products);
        }

        
        public IActionResult Upsert(int? id)
        {

            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if(id == null || id == 0)
            {
                //create
            
                return View(productVM);
            }
            else
            {
                // update

                productVM.Product = _unitOfWork.ProductRepository.GetT(c => c.Id == id);
                return View(productVM);
            }

            
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM obj,IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\product");
                    var extension = Path.GetExtension(file.FileName);
                    if (obj.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"\images\product\" + fileName + extension;
                }
                if (obj.Product.Id == 0)
                {
                    _unitOfWork.ProductRepository.Add(obj.Product);
                }
                else
                {
                    _unitOfWork.ProductRepository.Update(obj.Product);
                }


                    _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                obj.CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                
                return View(obj);

            }


        }



        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            Product? productFromDb = _unitOfWork.ProductRepository.GetT(c => c.Id == id);

            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? productFromDb = _unitOfWork.ProductRepository.GetT(c => c.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.ProductRepository.Remove(productFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index", "Product");  
          
        }


    }
}
