﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }

        public int? OrderID { get; set; }
        public Order? Order { get; set; }

        [StringLength(50)]
        public string? Method { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? PaymentDate { get; set; }
    }
}
