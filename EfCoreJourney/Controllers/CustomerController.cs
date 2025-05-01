using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerDal _customerDal;

        public CustomerController(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _customerDal.GetAll();  // await ile veriyi çek
            return View(customers);                       // customers artık List<Customer>
        }
    }
}
