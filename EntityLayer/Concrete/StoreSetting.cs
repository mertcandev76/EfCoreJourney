using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class StoreSetting
    {
        [Key]
        public int StoreSettingID { get; set; }

        public string? Currency { get; set; }
        public string? Language { get; set; }
        public bool EnableNotifications { get; set; }

        public int StoreId { get; set; }
        public Store? Store { get; set; }
    }
}
