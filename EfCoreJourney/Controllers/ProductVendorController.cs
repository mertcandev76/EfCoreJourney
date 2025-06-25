using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class ProductVendorController : Controller
    {
        private readonly IGenericService<ProductVendor> _vendorService;

        public ProductVendorController(IGenericService<ProductVendor> vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vendors = await _vendorService.GetAllAsync();
            return View(vendors);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductVendor productVendor)
        {
            if (ModelState.IsValid)
            {
                await _vendorService.AddAsync(productVendor);
                return RedirectToAction(nameof(Index));
            }
            return View(productVendor);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var brands = await _vendorService.GetByIdAsync(id);
            if (brands == null) return NotFound();
            return View(brands);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductVendor productVendor)
        {
            if (ModelState.IsValid)
            {
                await _vendorService.UpdateAsync(productVendor);
                return RedirectToAction(nameof(Index));
            }
            return View(productVendor);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var vendors = await _vendorService.GetByIdAsync(id);
            if (vendors == null) return NotFound();
            return View(vendors);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendors = await _vendorService.GetByIdAsync(id);
            if (vendors != null) await _vendorService.DeleteAsync(vendors);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Details(int id)
        {
            var vendors = await _vendorService.GetByIdAsync(id);
            if (vendors == null) return NotFound();
            return View(vendors);
        }


    }
}
