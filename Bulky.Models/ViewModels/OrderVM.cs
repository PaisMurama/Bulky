using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.ViewModels
{
    // Quando estamos a trablhar com o pedido
    // E queremos  algo relcaionado ao pedido
    // Devemos recuperar os detalhes associados ao pedido

    public class OrderVM
    {
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }

    }
}
