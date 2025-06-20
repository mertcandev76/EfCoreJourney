using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class ProductBrand
    {
        [Key]
        public int ProductBrandID { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Country { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
