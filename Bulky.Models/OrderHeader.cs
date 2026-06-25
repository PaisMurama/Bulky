using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BulkyBook.Models
{
    public class OrderHeader
    {
        // for the order header, we will use the following values: Id, ApplicationUserId, OrderDate, ShippingDate, OrderTotal, OrderStatus, PaymentStatus, TrackingNumber, Carrier, PaymentDate, PaymentDueDate, PaymentIntentId, PhoneNumber, StreetAddress, City, State, PostalCode, Name
        public int Id { get; set; }

        // for the application user, we will use the ApplicationUser class from the Identity framework
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]

        // for the application user, we will use the ApplicationUser class from the Identity framework
        public ApplicationUser ApplicationUser { get; set; }

        // for order date, we will use the current date and time when the order is created
        public DateTime OrderDate { get; set; }

        // for shipping date, we will set the shipping date to 7 days after the order date(company policy)
        public DateTime ShippingDate { get; set; }

        // for order total, we will use the following values: 0.00, 10.00, 20.00, etc.
        public double OrderTotal { get; set; }

        // for order status, we will use the following values: Pending, Approved, InProcess, Shipped, Cancelled, Refunded, etc.
        public string? OrderStatus { get; set; }
        //for payment status, we will use the following values: Pending, Approved, Rejected, Refunded, etc.
        public string? PaymentStatus { get; set; }

        // for shipping tracking number, we will use the tracking number provided by the shipping carrier
        public string? TrackingNumber { get; set; }
        // for shipping carrier, we will use UPS, FedEx, USPS, etc.
        public string? Carrier { get; set; }

        public DateTime PaymentDate { get; set; }

        // for user to pay the order, we will set the payment due date to 30 days after the order date(company policy)
        public DateOnly PaymentDueDate { get; set; }


        public string? SessionId { get; set; }

        // for stripe payment
        public string? PaymentIntentId { get; set; }

        // for phone number, we will use the following values: 123-456-7890, 987-654-3210, etc.
        [Required]
        public string PhoneNumber { get; set; }

        // for street address, we will use the following values: 123 Main St, 456 Elm St, etc.
        [Required]
        public string StreetAddress { get; set; }

        // for city, we will use the following values: New York, Los Angeles, Chicago, etc.
        [Required]
        public string City { get; set; }

        // for state, we will use the following values: NY, CA, IL, etc.
        [Required]
        public string State { get; set; }


        // for postal code, we will use the following values: 10001, 90001, 60601, etc.
        [Required]
        public string PostalCode { get; set; }

        // for name, we will use the following values: John Doe, Jane Smith, etc.
        [Required]
        public string Name { get; set; }




    }
}
