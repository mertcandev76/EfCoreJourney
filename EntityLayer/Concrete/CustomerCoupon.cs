using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class CustomerCoupon
    {
        public int CustomerCouponID { get; set; }

        public int CustomerID { get; set; }
        public Customer? Customer { get; set; }

        public int CouponID { get; set; }
        public Coupon? Coupon { get; set; }

        public DateTime? RedeemedAt { get; set; }
    }
}
