using DataAccessLayer.Abstract;
using EfCoreJourney.Models;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class Customer_GroupBy_Controller : Controller
    {
        private readonly ICustomerGroupBy_Dal _customerGroupBy_Dal;

        public Customer_GroupBy_Controller(ICustomerGroupBy_Dal customerGroupBy_Dal)
        {
            _customerGroupBy_Dal = customerGroupBy_Dal;
        }

        public async Task<IActionResult> GroupByCustomerList()
        {
            var model = new Customer_GroupBy_View_Model
            {
                AllCustomers=await _customerGroupBy_Dal.GetAllCustomersAsync(),
                GroupByAge = await _customerGroupBy_Dal.GetGroupByAgeAsync(),
                GroupByIsActive=await _customerGroupBy_Dal.GetGroupByIsActiveAsync(),
                GroupByEmailStatus=await _customerGroupBy_Dal.GetGroupByEmailStatusAsync(),
                GroupByAgeGroup=await _customerGroupBy_Dal.GetGroupByAgeGroupAsync(),
                CustomerCountByAgeGroup=await _customerGroupBy_Dal.GetCustomerCountByAgeGroupAsync(),
                GroupByCityFromAddress=await _customerGroupBy_Dal.GetGroupByCityFromAddressAsync()

            };
            return View(model);
        }
    }
}
