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

        [StringLength(100)]
        public string? ProductName { get; set; }

        public decimal? Price { get; set; }

        [StringLength(250)]
        public string? Description { get; set; }

        public int? Stock { get; set; }

        public bool? IsActive { get; set; }

        // Navigation Property
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
