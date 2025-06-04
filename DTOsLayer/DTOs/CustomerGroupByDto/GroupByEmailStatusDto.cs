using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsLayer.DTOs.CustomerGroupByDto
{
    public class GroupByEmailStatusDto
    {
        public bool? EmailStatus { get; set; }  // true = Email boş, false = Email dolu
        public int? Count { get; set; }
    }
}
