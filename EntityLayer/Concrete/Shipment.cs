using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Shipment
    {

        [Key]
        public int ShipmentID { get; set; }

        public int? OrderID { get; set; }
        public Order? Order { get; set; }

        [StringLength(100)]
        public string? ShipperName { get; set; }

        public DateTime? ShipmentDate { get; set; }

        public string? TrackingNumber { get; set; }

        public string? Status { get; set; }
    }
}
