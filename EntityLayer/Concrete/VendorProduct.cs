using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class VendorProduct
    {
        [Key]
        public int VendorProductID { get; set; }

        public int ProductID { get; set; } // foreign key
        public Product? Product { get; set; }

        public int ProductVendorID { get; set; } // foreign key
        public ProductVendor? ProductVendor { get; set; }
        public decimal SupplyPrice { get; set; }
        public DateTime? SuppliedDate { get; set; }

    }
}
