using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EfCoreJourney.Models;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class Customer_Conversion_and_Filtering_Controller : Controller
    {
        private readonly ICustomerConversion_and_Filtering_Dal _customerConversion_And_Filtering_Dal;

        public Customer_Conversion_and_Filtering_Controller(ICustomerConversion_and_Filtering_Dal customerConversion_And_Filtering_Dal)
        {
            _customerConversion_And_Filtering_Dal = customerConversion_And_Filtering_Dal;
        }

        public async Task<IActionResult> FilteredCustomerList()
        {
            /*
            //1.yol
            var All_Customerrnvrs= await _customerConversion_And_Filtering_Dal.GetAllCustomersAsync();
            var Active_Customers = await _customerConversion_And_Filtering_Dal.GetActiveCustomersAsync();
            var model=new Customer_Conversion_and_Filtering_List_View_Model
            {
                AllCustomers = All_Customerrnvrs,
                ActiveCustomers=Active_Customers
                // diğerleri de aynı şekilde...

            };
            return View(model);
            */

            //2.yol
            var model = new Customer_Conversion_and_Filtering_List_View_Model
            {
               AllCustomers = await _customerConversion_And_Filtering_Dal.GetAllCustomersAsync(),
               ActiveCustomers = await _customerConversion_And_Filtering_Dal.GetActiveCustomersAsync(),
               InactiveCustomers = await _customerConversion_And_Filtering_Dal.GetInactiveCustomersAsync(),
               CustomersOlderThan30 = await _customerConversion_And_Filtering_Dal.GetCustomersOlderThan30Async(),
               MinorCustomers = await _customerConversion_And_Filtering_Dal.GetMinorCustomersAsync(),
               CustomersWithPhone = await _customerConversion_And_Filtering_Dal.GetCustomersWithPhoneAsync(),
               CustomersWithoutPhone=await _customerConversion_And_Filtering_Dal.GetCustomersWithoutPhoneAsync(),
               CustomersNamedAli=await _customerConversion_And_Filtering_Dal.GetCustomersNamedAliAsync(),
               CustomersStartsWith=await _customerConversion_And_Filtering_Dal.GetCustomersStartsWithAAsync(),
               CustomersWithAge=await _customerConversion_And_Filtering_Dal.GetCustomersWithAgeAsync(),
               CustomersWithNullAge=await _customerConversion_And_Filtering_Dal.GetCustomersWithNullAgeAsync(),
               ActiveYoungCustomers=await _customerConversion_And_Filtering_Dal.GetActiveYoungCustomersAsync(),
               CustomersWithLongLastName=await _customerConversion_And_Filtering_Dal.GetCustomersWithLongLastNameAsync(),
               CustomersWithSpecificAges=await _customerConversion_And_Filtering_Dal.GetCustomersWithSpecificAgesAsync(),
               CustomersWithoutGmailComEmail = await _customerConversion_And_Filtering_Dal.GetCustomersWithoutGmailComEmailAsync()



            };

            return View(model);
        }
            
        
    }
}
