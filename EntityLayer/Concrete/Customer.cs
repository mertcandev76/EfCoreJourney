using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; } = null!;

        public int? Age { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? Phone { get; set; }

        public string? Address { get; set; }
        public bool? IsActive { get; set; }
        public DateTime RegisteredDate { get; set; } = DateTime.Now;

        public ICollection<Order>? Orders { get; set; }
        public ICollection<CustomerCoupon>? CustomerCoupons { get; set; }
    }
}
