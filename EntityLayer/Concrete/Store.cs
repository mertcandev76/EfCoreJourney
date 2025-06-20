using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EntityLayer.Concrete
{
    public class Store
    {
        [Key]
        public int StoreID { get; set; }

        public string? Name { get; set; }
        public string? OwnerName { get; set; }
        public string? Email { get; set; }

        public StoreSetting? StoreSetting { get; set; }
    }
}
