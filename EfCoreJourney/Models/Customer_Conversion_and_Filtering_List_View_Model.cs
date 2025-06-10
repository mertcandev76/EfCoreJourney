using EntityLayer.Concrete;

namespace EfCoreJourney.Models
{
    public class Customer_Conversion_and_Filtering_List_View_Model
    {
        public List<OrderCustomer> AllCustomers { get; set; }
        public List <OrderCustomer> ActiveCustomers { get; set; }
        public List<OrderCustomer> InactiveCustomers { get; set; }
        public List<OrderCustomer> CustomersOlderThan30 { get; set; }
        public List<OrderCustomer> MinorCustomers { get; set; }
        public List<OrderCustomer> CustomersWithPhone { get; set; }
        public List<OrderCustomer> CustomersWithoutPhone { get; set; }
        public List<OrderCustomer> CustomersNamedAli { get; set; }
        public List<OrderCustomer> CustomersStartsWith { get; set; }
        public List<OrderCustomer> CustomersWithAge { get; set; }
        public List<OrderCustomer> CustomersWithNullAge { get; set; }
        public List<OrderCustomer> ActiveYoungCustomers { get; set; }
        public List<OrderCustomer> CustomersWithLongLastName { get; set; }
        public List<OrderCustomer> CustomersWithSpecificAges { get; set; }
        public List<OrderCustomer> CustomersWithoutGmailComEmail { get; set; }
        





    }
}
