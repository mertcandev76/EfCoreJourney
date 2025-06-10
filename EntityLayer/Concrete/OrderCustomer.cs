using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class OrderCustomer
    {


      [Key]
    public int OrderCustomerID { get; set; }

    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    public int? Age { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public bool? IsActive { get; set; }

    // Navigation Property (bire-çok)
    public ICollection<Order> Orders { get; set; } = new List<Order>();

    }

}
