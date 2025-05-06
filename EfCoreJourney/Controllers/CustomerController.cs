using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EfCoreJourney.Models;
using EntityLayer.Concrete;
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
            var customers = await _customerDal.GetAll();
            var singleCustomer = await _customerDal.GetSingleCustomerOperationAsync();
            var customerCount = await _customerDal.GetCustomerStatisticsAsync();
            var exist = await _customerDal.CustomerExistsAsync();
            var customerSum = await _customerDal.GetValueAsync();

            var model = new CustomerListViewModel
            {
                Customers = customers,
                RepresentativeCustomer=singleCustomer,
                TotalCustomerCount=customerCount,
                IsCustomerExist=exist,
                TotalCustomerSum=customerSum
            };
            return View(model);                      
        }

        // Müşteri ekleme (GET)
        [HttpGet]
        public IActionResult AddCustomer()
        {
            return View();
        }

        // Müşteri ekleme (POST)
        [HttpPost]
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            if (ModelState.IsValid)  // Verinin geçerli olup olmadığını kontrol et
            {
                await _customerDal.InsertAsync(customer);  // Veriyi asenkron olarak veri katmanına gönder
                return RedirectToAction("Index");  // Başarılıysa kullanıcıyı Index sayfasına yönlendir
            }

            return View(customer);  // Eğer veri geçerli değilse, aynı sayfaya geri dön
        }


    }
}
