using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class CouponController : Controller
    {
        private readonly IGenericService<Coupon> _couponService;

        public CouponController(IGenericService<Coupon> couponService)
        {
            _couponService = couponService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var coupons = await _couponService.GetAllAsync();
            return View(coupons);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Coupon coupon)
        {
            if (ModelState.IsValid)
            {
                await _couponService.AddAsync(coupon);
                return RedirectToAction(nameof(Index));
            }
            return View(coupon);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var coupons = await _couponService.GetByIdAsync(id);
            if (coupons == null) return NotFound();
            return View(coupons);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Coupon coupon)
        {
            if (ModelState.IsValid)
            {
                await _couponService.UpdateAsync(coupon);
                return RedirectToAction(nameof(Index));
            }
            return View(coupon);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var coupons = await _couponService.GetByIdAsync(id);
            if (coupons == null) return NotFound();
            return View(coupons);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coupons = await _couponService.GetByIdAsync(id);
            if (coupons != null) await _couponService.DeleteAsync(coupons);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Details(int id)
        {
            var coupons = await _couponService.GetByIdAsync(id);
            if (coupons == null) return NotFound();
            return View(coupons);
        }
    }
}
