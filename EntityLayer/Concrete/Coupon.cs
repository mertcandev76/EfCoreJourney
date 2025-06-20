using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Coupon
    {

        [Key]
        public int CouponID { get; set; }

        public string? Code { get; set; }
        public decimal DiscountRate { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }
        public bool IsActive { get; set; }

        public ICollection<CustomerCoupon>? CustomerCoupons { get; set; }
    }
}
