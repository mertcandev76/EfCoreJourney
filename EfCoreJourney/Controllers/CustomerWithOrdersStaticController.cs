using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class CustomerWithOrdersStaticController : Controller
    {
        private readonly ICustomerWithOrdersStaticRepository _customerWithOrdersStaticRepository;

        public CustomerWithOrdersStaticController(ICustomerWithOrdersStaticRepository customerWithOrdersStaticRepository)
        {
            _customerWithOrdersStaticRepository = customerWithOrdersStaticRepository;
        }

        public async Task<IActionResult> Index()
        {
            var customers= await _customerWithOrdersStaticRepository.GetAllAsync();
            return View(customers);
        }
        public async Task<IActionResult> GetCustomerById()
        {
            var customers = await _customerWithOrdersStaticRepository.GetByIdAsync();
            if (customers==null)            
                return NotFound("Sabit ID'li müşteri bulunamadı.");
            
            return View(customers);
        }
        public async Task<IActionResult> AddCustomerWithOrders()
        {
            await _customerWithOrdersStaticRepository.AddAsync();
            TempData["Message"] = "Müşteri ve siparişleri başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateCustomerWithOrders()
        {
            await _customerWithOrdersStaticRepository.UpdateAsync();
            TempData["Message"] = "Müşteri ve siparişleri başarıyla Güncellendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteCustomerWithOrders()
        {
            await _customerWithOrdersStaticRepository.DeleteAsync();
            TempData["Message"] = "Müşteri ve siparişleri başarıyla Silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
