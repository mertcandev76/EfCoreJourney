using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class OrderPayment
    {
        [Key]
        public int OrderPaymentID { get; set; }

        public decimal Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int OrderID { get; set; }
        public Order? Order { get; set; }
    }
}
