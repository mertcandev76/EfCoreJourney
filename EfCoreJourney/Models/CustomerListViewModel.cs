using EntityLayer.Concrete;

namespace EfCoreJourney.Models
{
    public class CustomerListViewModel
    {
        // Tüm müşterilerin listesi
        public List<Customer> Customers { get; set; }

        // First/Single gibi sorgularla alınan tek müşteri örneği
        public Customer RepresentativeCustomer { get; set; }

        // Müşteri sayısı (Count)
        public int TotalCustomerCount { get; set; }

        // Müşteri var mı? (bool)
        public bool IsCustomerExist { get; set; }
    }
}
