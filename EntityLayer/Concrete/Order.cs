using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Order
    {

        [Key]
        public int OrderID { get; set; }

        public DateTime OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? Status { get; set; }// Pending, Shipped, Delivered, Cancelled
        public string? Notes { get; set; }

        public int CustomerID { get; set; } //foreign Key
        public Customer? Customer { get; set; }

        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public OrderPayment? OrderPayment { get; set; }
        public OrderShipment? OrderShipment { get; set; }
    }
}
