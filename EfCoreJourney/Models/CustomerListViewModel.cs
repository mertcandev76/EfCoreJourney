using EntityLayer.Concrete;

namespace EfCoreJourney.Models
{
    public class CustomerListViewModel
    {
        // Tüm müşterilerin listesi
        public List<OrderCustomer> Customers { get; set; }

        // First/Single gibi sorgularla alınan tek müşteri örneği
        public OrderCustomer RepresentativeCustomer { get; set; }

        // Müşteri sayısı (Count)
        public int TotalCustomerCount { get; set; }

        // Müşteri var mı? (bool)
        public bool IsCustomerExist { get; set; }
        // Müşterilerin yaş toplamı(Sum)
        public decimal? TotalCustomerSum { get; set; }
    }
}
