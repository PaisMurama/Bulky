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
    public class OrderDetail
    {
        public int Id { get; set; }


        [Required]
        // for the order header, we will use the OrderHeader class from the OrderHeader model
        public int OrderHeaderId { get; set; }

        [ForeignKey("OrderHeaderId")]
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }

        // for count, we will use the following values: 1, 2, 3, etc.
        public int Count { get; set; }

        // for price, we will use the following values: 0.00, 10.00, 20.00, etc.
        public double Price { get; set; }







    }
}
