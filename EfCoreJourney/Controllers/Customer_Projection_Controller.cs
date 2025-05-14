using DataAccessLayer.Abstract;
using DTOsLayer.DTOs;
using EfCoreJourney.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class Customer_Projection_Controller : Controller
    {
        private readonly ICustomerProjection_Dal _customerProjection_Dal;

        public Customer_Projection_Controller(ICustomerProjection_Dal customerProjection_Dal)
        {
            _customerProjection_Dal = customerProjection_Dal;
        }

        public async Task<IActionResult> CustomerProjection()
        {
            var model = new Customer_Projection_List_View_Model()
            {
                Customers =await _customerProjection_Dal.GetAllCustomersAsync(),
                CustomerFullNames=await _customerProjection_Dal.GetCustomerFullNamesAsync(),
                ActiveCustomers=await _customerProjection_Dal.GetActiveCustomersAsync(),
                CustomersOlderThan=await _customerProjection_Dal.GetCustomersOlderThan30Async(),
                InactiveCustomers=await _customerProjection_Dal.GetInactiveCustomersAsync(),
                OlderThan25Customers=await _customerProjection_Dal.GetOlderThan25CustomersAsync(),
                CustomerDetails=await _customerProjection_Dal.GetCustomerDetailsAsync(),
                CustomerPhoneLength=await _customerProjection_Dal.GetCustomerPhoneLengthAsync(),
                CustomerFirstNameUpperCase=await _customerProjection_Dal.GetCustomerFirstNameUpperCaseAsync()
              

            };
            return View(model);
        }
    }
}
