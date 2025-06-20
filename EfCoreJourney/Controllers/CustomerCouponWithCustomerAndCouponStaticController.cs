using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class CustomerCouponWithCustomerAndCouponStaticController : Controller
    {
        private readonly ICustomerCouponWithCustomerAndCouponStaticRepository _customerCouponWithCustomerAndCouponStaticRepository;

        public CustomerCouponWithCustomerAndCouponStaticController(ICustomerCouponWithCustomerAndCouponStaticRepository customerCouponWithCustomerAndCouponStaticRepository)
        {
            _customerCouponWithCustomerAndCouponStaticRepository = customerCouponWithCustomerAndCouponStaticRepository;
        }

        public async Task<IActionResult> Index()
        {
            var customerCoupons= await _customerCouponWithCustomerAndCouponStaticRepository.GetAllAsync();
            return View(customerCoupons);
        }
        public async Task<IActionResult> GetByID()
        {
            var customerCoupons = await _customerCouponWithCustomerAndCouponStaticRepository.GetByIdAsync();
            if (customerCoupons == null)
                return NotFound("Belirtilen müşteri kuponu bulunamadı.");
            return View(customerCoupons);
        }
        public async Task<IActionResult> AddCustomerCoupon()
        {
            await _customerCouponWithCustomerAndCouponStaticRepository.AddAsync();
            TempData["Message"] = "Müşteri kuponu başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateCustomerCoupon()
        {
            await _customerCouponWithCustomerAndCouponStaticRepository.UpdateAsync();
            TempData["Message"] = "Müşteri kuponu başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteCustomerCoupon()
        {
            await _customerCouponWithCustomerAndCouponStaticRepository.DeleteAsync();
            TempData["Message"] = "Müşteri kuponu başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
