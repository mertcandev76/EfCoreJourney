using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Log
    {
        //İliŞKİSİZ (BAĞIMSIZ) ✅✅✅
        [Key]
        public int LogID { get; set; }

        public DateTime? LogDate { get; set; }

        [StringLength(100)]
        public string? LogLevel { get; set; }

        public string? Message { get; set; }
        public string? Details { get; set; }
        public string? Source { get; set; }
        public string? User { get; set; }
    }
}
