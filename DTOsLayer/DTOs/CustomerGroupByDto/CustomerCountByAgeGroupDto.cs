using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer.DTOs.CustomerGroupByDto
{
    public class CustomerCountByAgeGroupDto
    {
        public string Grup { get; set; } = string.Empty; // Genç, Orta, Yaşlı
        public int? Count { get; set; } // Grup içindeki müşteriler
    }
}
