using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EfCoreJourney.Controllers
{
    public class CustomerCouponController : Controller
    {
        private readonly ICustomerCouponService _customerCouponService;
        private readonly IGenericService<Customer> _customerService;
        private readonly IGenericService<Coupon> _couponService;
        public CustomerCouponController(ICustomerCouponService customerCouponService, IGenericService<Customer> customerService, IGenericService<Coupon> couponService)
        {
            _customerCouponService = customerCouponService;
            _customerService = customerService;
            _couponService = couponService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var customerCoupons = await _customerCouponService.GetAllWithCustomerandCouponAsync();
            return View(customerCoupons);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Customers = new SelectList(await _customerService.GetAllAsync(), "CustomerID", "FullName");
            ViewBag.Coupons = new SelectList(await _couponService.GetAllAsync(), "CouponID", "Code");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CustomerCoupon customerCoupon)
        {
            if (ModelState.IsValid)
            {
                await _customerCouponService.AddAsync(customerCoupon);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Customers = new SelectList(await _customerService.GetAllAsync(), "CustomerID", "FullName");
            ViewBag.Coupons = new SelectList(await _couponService.GetAllAsync(), "CouponID", "Code");
            return View(customerCoupon);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var customerCoupons = await _customerCouponService.GetByIdWithCustomerandCouponAsync(id);
            if (customerCoupons == null) return NotFound();
            ViewBag.Customers = new SelectList(await _customerService.GetAllAsync(), "CustomerID", "FullName",customerCoupons.CustomerID);
            ViewBag.Coupons = new SelectList(await _couponService.GetAllAsync(), "CouponID", "Code",customerCoupons.CouponID);
            return View(customerCoupons);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerCoupon customerCoupon)
        {
            if (ModelState.IsValid)
            {
                await _customerCouponService.UpdateAsync(customerCoupon);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Customers = new SelectList(await _customerService.GetAllAsync(), "CustomerID",     "FullName",customerCoupon.CustomerID);
            ViewBag.Coupons = new SelectList(await _couponService.GetAllAsync(), "CouponID", "Code", customerCoupon.CouponID);
            return View(customerCoupon);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var customerCoupons = await _customerCouponService.GetByIdWithCustomerandCouponAsync(id);
            if (customerCoupons == null) return NotFound();

            return View(customerCoupons);
        }

        // POST: Silme işlemi
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerCoupons = await _customerCouponService.GetByIdAsync(id);
            if (customerCoupons != null)
            {
                await _customerCouponService.DeleteAsync(customerCoupons);
            }

            return RedirectToAction(nameof(Index));
        }
        // Detaylar
        public async Task<IActionResult> Details(int id)
        {
            var customerCoupons = await _customerCouponService.GetByIdWithCustomerandCouponAsync(id);
            if (customerCoupons == null) return NotFound();

            return View(customerCoupons);
        }
    }
}
