
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
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


        public IActionResult Index()
        {
            var customers = _customerDal.GetAll();
            return View(customers);
        }

        //Müşteri ekleme
        [HttpGet]
        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            if (ModelState.IsValid)  // Verinin geçerli olup olmadığını kontrol et
            {
                _customerDal.Insert(customer);  // Veriyi veri katmanına gönder
                return RedirectToAction("Index");  // Başarılıysa kullanıcıyı Index sayfasına yönlendir
            }

            return View(customer);  // Eğer veri geçerli değilse, aynı sayfaya geri dön
        }

        //Müşteri Güncelleme

        [HttpGet]
        public IActionResult UpdateCustomer(int id)
        {
            var existingCustomer = _customerDal.GetById(id);
            if (existingCustomer == null)
            {
                return NotFound(); // Müşteri bulunamazsa 404 döner
            }

            return View(existingCustomer); // Formda göstermek için müşteriyi gönderiyoruz
        }

        [HttpPost]
        public IActionResult UpdateCustomer(Customer customer)
        {
            var existingCustomer = _customerDal.GetById(customer.CustomerID);
            if (existingCustomer == null)
            {
                return NotFound(); // Yine id yanlışsa veya müşteri yoksa 404
            }

            if (ModelState.IsValid)
            {
                _customerDal.Update(customer); // Veritabanında güncelleme
                return RedirectToAction("Index"); // Güncelleme sonrası listeye dön
            }

            return View(customer); // Hatalıysa formu tekrar göster
        }


        // Sabit veri ile müşteri silme
        public IActionResult DeleteCustomer(int id)
        {
           
            var customer = _customerDal.GetById(id);
            // Müşteri bulunamazsa hata mesajı döndürülebilir
            if (customer == null)
            {
                return NotFound("Müşteri bulunamadı");
            }
            _customerDal.Delete(customer); // ID üzerinden silme
            return RedirectToAction("Index");
        }


    }
}
