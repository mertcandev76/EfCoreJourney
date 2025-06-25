using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class ProductBrandController : Controller
    {
        private readonly IGenericService<ProductBrand> _brandService;

        public ProductBrandController(IGenericService<ProductBrand> brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var brands= await _brandService.GetAllAsync();
            return View(brands);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductBrand productBrand)
        {
            if (ModelState.IsValid)
            {
                await _brandService.AddAsync(productBrand);
                return RedirectToAction(nameof(Index));
            }
            return View(productBrand);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var brands= await _brandService.GetByIdAsync(id);
            if (brands == null) return NotFound();
            return View(brands);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductBrand productBrand)
        {
            if (ModelState.IsValid)
            {
                await _brandService.UpdateAsync(productBrand);
                return RedirectToAction(nameof(Index));
            }
            return View(productBrand);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var brands = await _brandService.GetByIdAsync(id);
            if (brands == null) return NotFound();
            return View(brands);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brands = await _brandService.GetByIdAsync(id);
            if (brands != null) await _brandService.DeleteAsync(brands);
            return RedirectToAction(nameof(Index));
        }
        

        public async Task<IActionResult> Details(int id)
        {
            var brands = await _brandService.GetByIdAsync(id);
            if (brands == null) return NotFound();
            return View(brands);
        }

    }
}
