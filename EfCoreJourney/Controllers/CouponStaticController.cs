using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class CouponStaticController : Controller
    {
        private readonly ICouponStaticRepository _couponStaticRepository;

        public CouponStaticController(ICouponStaticRepository couponStaticRepository)
        {
            _couponStaticRepository = couponStaticRepository;
        }

        public async Task<IActionResult> Index()
        {
            var coupons= await _couponStaticRepository.GetAllAsync();
            return View(coupons);
        }
        public async Task<IActionResult> GetByID()
        {
            var coupons = await _couponStaticRepository.GetByIdAsync();
            if (coupons == null) return NotFound("Belirtilen kupon bulunamadı.");
            return View(coupons);
        }
        public async Task<IActionResult> AddCoupon()
        {
            await _couponStaticRepository.AddAsync();
            TempData["Message"] = "Kupon başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateCoupon()
        {
            await _couponStaticRepository.UpdateAsync();
            TempData["Message"] = "Kupon başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteCoupon()
        {
            await _couponStaticRepository.DeleteAsync();
            TempData["Message"] = "Kupon başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
