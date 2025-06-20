using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class CustomerStaticController : Controller
    {
        private readonly ICustomerStaticRepository _customerStaticRepository;

        public CustomerStaticController(ICustomerStaticRepository customerStaticRepository)
        {
            _customerStaticRepository = customerStaticRepository;
        }

        public async Task<IActionResult> Index()
        {
            var customers= await _customerStaticRepository.GetAllAsync();
            return View(customers);
        }
        public async Task<IActionResult> GetByID()
        {
            var customers = await _customerStaticRepository.GetByIdAsync();
            if (customers == null) return NotFound("Belirtilen müşteri bulunamadı.");
            return View(customers);
        }
        public async Task<IActionResult> AddCustomer()
        {
            await _customerStaticRepository.AddAsync();
            TempData["Message"] = "Müşteri başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateCustomer()
        {
            await _customerStaticRepository.UpdateAsync();
            TempData["Message"] = "Müşteri bilgileri başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteCustomer()
        {
            await _customerStaticRepository.DeleteAsync();
            TempData["Message"] = "Müşteri başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
