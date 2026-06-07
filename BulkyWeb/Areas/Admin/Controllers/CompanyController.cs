using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CompanyController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Company> companies = _unitOfWork.CompanyRepository.GetAll().ToList();
            return View(companies);
        }

        
        public IActionResult Upsert(int? id)
        {

            CompanyVM companyVM = new CompanyVM()
            {
                Company = new Company()
            };

            if(id == null || id == 0)
            {
                //create
            
                return View(companyVM);
            }
            else
            {
                // update

                companyVM.Company = _unitOfWork.CompanyRepository.GetT(c => c.Id == id);
                return View(companyVM);
            }

            
        }

        [HttpPost]
        public IActionResult Upsert(CompanyVM obj)
        {

            if (ModelState.IsValid)
            {
          
                if (obj.Company.Id == 0)
                {
                    _unitOfWork.CompanyRepository.Add(obj.Company);
                }
                else
                {
                    _unitOfWork.CompanyRepository.Update(obj.Company);
                }

                 _unitOfWork.Save();
                TempData["success"] = "Company created successfully";
                return RedirectToAction("Index", "Company");
            }
            else
            {
 
                return View(obj);

            }


        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.CompanyRepository.GetAll().ToList();
            return Json(new {data = objCompanyList });
        }


        [HttpDelete]
        public IActionResult Delete (int? id)
        {
            var companyToBeDeleted = _unitOfWork.CompanyRepository.GetT(u => u.Id == id);
            if (companyToBeDeleted == null)
            {
                return Json(new { success=false, message = "Error while deleting"});
            }

            
            _unitOfWork.CompanyRepository.Remove(companyToBeDeleted);
            _unitOfWork.Save();


            return   Json(new { success = true, message = "Delete Successful" });
        }


        #endregion

    }
}
