using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        public DateTime? OrderDate { get; set; }

        public decimal? TotalAmount { get; set; } // siparişin toplam tutarı

        [StringLength(100)]
        public string? ShippingAddress { get; set; } // teslimat adresi

        [StringLength(50)]
        public string? PaymentMethod { get; set; } // Nakit, Kredi Kartı vb.

        // Foreign Key
        public int? OrderCustomerID { get; set; }

        // Navigation Property
        public OrderCustomer? OrderCustomer { get; set; }
    }
}
