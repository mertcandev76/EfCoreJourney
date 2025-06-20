using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class ProductBrandStaticController : Controller
    {
        private readonly IProductBrandStaticRepository  _productsBrandStaticRepository;

        public ProductBrandStaticController(IProductBrandStaticRepository productsBrandWithProductsStaticRepository)
        {
            _productsBrandStaticRepository = productsBrandWithProductsStaticRepository;
        }

        public async Task<IActionResult> Index()
        {
            var productBrands= await _productsBrandStaticRepository.GetAllAsync();
            return View(productBrands);
        }
        public async Task<IActionResult> GetByID()
        {
            var productBrands = await _productsBrandStaticRepository.GetByIdAsync();
            if (productBrands == null)
            {
                return NotFound("Belirtilen ürün markası bulunamadı.");
            }
            return View(productBrands);
        }
        public async Task<IActionResult> AddProductBrand()
        {
            await _productsBrandStaticRepository.AddAsync();
            TempData["Message"] = "Ürün markası başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateProductBrand()
        {
            await _productsBrandStaticRepository.UpdateAsync();
            TempData["Message"] = "Ürün markası başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteProductBrand()
        {
            await _productsBrandStaticRepository.DeleteAsync();
            TempData["Message"] = "Ürün markası başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
