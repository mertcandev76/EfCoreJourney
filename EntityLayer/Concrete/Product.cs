using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; }

        public int ProductBrandID { get; set; }
        public ProductBrand? ProductBrand { get; set; }

        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public ICollection<VendorProduct>? VendorProducts { get; set; }
    }
}
