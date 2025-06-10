using DTOsLayer.DTOs.CustomerGroupByDto;
using EntityLayer.Concrete;

namespace EfCoreJourney.Models
{
    public class Customer_GroupBy_View_Model
    {
        public List<OrderCustomer> AllCustomers { get; set; }
        public List<GroupByAgeDto> GroupByAge { get; set; }
        public List<GroupByIsActiveDto> GroupByIsActive { get; set; }
        public List<GroupByEmailStatusDto> GroupByEmailStatus { get; set; }
        public List<GroupByAgeGroupDto> GroupByAgeGroup { get; set; }
        public List<CustomerCountByAgeGroupDto> CustomerCountByAgeGroup { get; set; }
        public List<object> GroupByFullName { get; set; }
        public List<object> GroupByPhoneStatus { get; set; }
        public List<GroupByCityFromAddressDto> GroupByCityFromAddress { get; set; }
        public List<object> GroupByIsActiveWithAvgAge { get; set; }
        public List<object> GroupByEmailDomain { get; set; }
        public List<object> GroupByFirstName { get; set; }
        public List<object> GroupByIsActiveWithAvg { get; set; }
        public List<object> GroupByAgeRange { get; set; }
        public List<object> GroupByEmptyFirstName { get; set; }
        public List<object> GroupByAddress { get; set; }
        public List<object> GroupByFullNameLength { get; set; }
        public List<object> GroupByEmailFirstChar { get; set; }
        public List<object> GroupByAgeList { get; set; }
        public List<object> GroupByPhoneLength { get; set; }
        public List<object> GroupByIsActiveAndAge { get; set; }
    }
}
