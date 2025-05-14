using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer.DTOs
{
    public class CustomerNameAndAgeCategoryDto
    {

        public string? FullName { get; set; }
        public string? AgeCategory { get; set; } // Örn: "Genç", "Orta Yaş", "Yaşlı"
    }
}
