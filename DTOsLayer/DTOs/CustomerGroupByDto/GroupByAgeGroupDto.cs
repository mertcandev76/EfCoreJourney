using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer.DTOs.CustomerGroupByDto
{
    public  class GroupByAgeGroupDto
    {
        public string Grup { get; set; } = string.Empty; // Genç, Orta, Yaşlı
        public List<OrderCustomer> Customers { get; set; } = new(); // Grup içindeki müşteriler
    }
}
