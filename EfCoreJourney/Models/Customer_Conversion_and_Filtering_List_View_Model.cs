using EntityLayer.Concrete;

namespace EfCoreJourney.Models
{
    public class Customer_Conversion_and_Filtering_List_View_Model
    {
        public List<Customer> AllCustomers { get; set; }
        public List <Customer> ActiveCustomers { get; set; }
        public List<Customer> InactiveCustomers { get; set; }
        public List<Customer> CustomersOlderThan30 { get; set; }
        public List<Customer> MinorCustomers { get; set; }
        public List<Customer> CustomersWithPhone { get; set; }
        public List<Customer> CustomersWithoutPhone { get; set; }
        public List<Customer> CustomersNamedAli { get; set; }
        public List<Customer> CustomersStartsWith { get; set; }
        public List<Customer> CustomersWithAge { get; set; }
        public List<Customer> CustomersWithNullAge { get; set; }
        public List<Customer> ActiveYoungCustomers { get; set; }
        public List<Customer> CustomersWithLongLastName { get; set; }
        public List<Customer> CustomersWithSpecificAges { get; set; }
        public List<Customer> CustomersWithoutGmailComEmail { get; set; }
        





    }
}
