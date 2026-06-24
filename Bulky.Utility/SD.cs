using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{
    /*
     Essa classe é uma classe de utilidade (Utility) que pode ser usada para armazenar constantes, métodos auxiliares ou qualquer outra funcionalidade que seja amplamente utilizada em todo o projeto.
     O nome "SD" é genérico e pode ser substituído por um nome mais descritivo, dependendo do contexto do projeto.
     */
    public class SD
    {
        public const string Role_Customer = "Customer";
        public const string Role_Company = "Company";
        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";

        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusInProcessing = "Processing";
        public const string StatusShipped = "Shipped";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";
        
        
        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusApprovedForDelayedPayment = "ApprovedForDelayedPayment";
        public const string PaymentStatusRejected = "Rejected";



    }
}
