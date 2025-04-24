
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
            var customers= _customerDal.GetAll();
            return View(customers);
        }


        public IActionResult AddStaticCustomer()
        {
            // Boş bir Customer nesnesi oluşturuluyor
            Customer customer = new Customer();

            // EfCustomerRepository'deki Insert metodunda sabit veriler atanacak
            _customerDal.Insert(customer);

            // Ekleme sonrası listeye dön
            return RedirectToAction("Index"); 
        }

        // Sabit veri ile müşteri güncelleme
        public IActionResult UpdateStaticCustomer(int id)
        {
            id = 2;
            // Müşteri verisini id'ye göre al
            var customer = _customerDal.GetById(id);

            if (customer == null)
            {
                return NotFound();  // Müşteri bulunamazsa hata döndür
            }

            // EfCustomerRepository'deki Update metodunu çağırıyoruz, sabit verilerle güncelleme yapılacak
            _customerDal.Update(customer);

            // Güncelleme sonrası listeye dön
            return RedirectToAction("Index");
        }

        // Sabit veri ile müşteri silme
        public IActionResult DeleteStaticCustomer(int id)
        {
            id = 3;
            var customer = _customerDal.GetById(id);
            _customerDal.Delete(customer); // ID üzerinden silme
            return RedirectToAction("Index");
        }


    }
}
