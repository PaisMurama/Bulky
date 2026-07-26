using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;  
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public OrderVM OrderVM { get; set; } 
        public OrderController(IUnitOfWork unitOfWork) { 
        _unitOfWork = unitOfWork;
            
        }
        public IActionResult Index()
        { 
            return View();
        }


        public IActionResult Details( int orderId)
        {
            OrderVM = new OrderVM()
            {
                OrderHeader = _unitOfWork.OrderHeaderRepository.GetT(u => u.Id == orderId, includeProperties: "ApplicationUser"),
                OrderDetails = _unitOfWork.OrderDetailRepository.GetAll(u => u.OrderHeaderId == orderId, includeProperties: "Product")
            };


            return View(OrderVM);
        }

        [Authorize(Roles =SD.Role_Admin+","+SD.Role_Employee)]
        public IActionResult UpdateOrderDetail()
        {
            var orderHeaderFromdb = _unitOfWork.OrderHeaderRepository.GetT(u=>u.Id == OrderVM.OrderHeader.Id);

            orderHeaderFromdb.Name = OrderVM.OrderHeader.Name;
            orderHeaderFromdb.PhoneNumber = OrderVM.OrderHeader.PhoneNumber;
            orderHeaderFromdb.StreetAddress = OrderVM.OrderHeader.StreetAddress;
            orderHeaderFromdb.City = OrderVM.OrderHeader.City;
            orderHeaderFromdb.State = OrderVM.OrderHeader.State;
            orderHeaderFromdb.PostalCode = OrderVM.OrderHeader.PostalCode;
            if (!string.IsNullOrEmpty(OrderVM.OrderHeader.Carrier))
            {
                orderHeaderFromdb.Carrier = OrderVM.OrderHeader.Carrier;
            }

            if (!string.IsNullOrEmpty(OrderVM.OrderHeader.TrackingNumber))
            {
                orderHeaderFromdb.Carrier = OrderVM.OrderHeader.TrackingNumber;
            }

            _unitOfWork.OrderHeaderRepository.Update(orderHeaderFromdb);
            _unitOfWork.Save();
            TempData["Success"] = "Order Details Updated Successfully.";

            return RedirectToAction(nameof(Details), new { orderId = orderHeaderFromdb.Id});
        }




        #region API CALLS

        [HttpGet]
        public IActionResult GetAll(string status)
        {
            List<OrderHeader> objOrderHeader = null;

            if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee))
            {
                objOrderHeader = _unitOfWork.OrderHeaderRepository.GetAll(includeProperties: "ApplicationUser").ToList();

            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                objOrderHeader = _unitOfWork.OrderHeaderRepository.GetAll(u => u.ApplicationUserId == userId, includeProperties: "ApplicationUser").ToList();

            }

                switch (status)
                {
                    case "pending":
                        objOrderHeader = objOrderHeader.Where(u => u.PaymentStatus == SD.PaymentStatusDelayedPayment).ToList();
                        break;

                    case "inprocess":
                        objOrderHeader = objOrderHeader.Where(u => u.OrderStatus == SD.StatusInProcessing).ToList();
                        break;

                    case "completed":
                        objOrderHeader = objOrderHeader.Where(u => u.OrderStatus == SD.StatusShipped).ToList();
                        break;
                    case "approved":
                        objOrderHeader = objOrderHeader.Where(u => u.OrderStatus == SD.StatusApproved).ToList();
                        break;

                    default:
                        break;
                }

            return Json(new { data = objOrderHeader });




        }


        #endregion

    }

}

