using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class ProductWithProductBrandStaticController : Controller
    {
        private readonly IProductWithProductBrandStaticRepository _productWithProductBrandStaticRepository;

        public ProductWithProductBrandStaticController(IProductWithProductBrandStaticRepository productWithProductBrandStaticRepository)
        {
            _productWithProductBrandStaticRepository = productWithProductBrandStaticRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products= await _productWithProductBrandStaticRepository.GetAllAsync();
            return View(products);
        }
        public async Task<IActionResult> GetById()
        {
            var products = await _productWithProductBrandStaticRepository.GetByIdAsync();
            if (products==null)
                return NotFound("Belirtilen ürün bulunamadı.");

            return View(products);
        }
        public async Task<IActionResult> AddProduct()
        {
            await _productWithProductBrandStaticRepository.AddAsync();
            TempData["Message"] = "Ürün başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateProduct()
        {
            await _productWithProductBrandStaticRepository.UpdateAsync();
            TempData["Message"] = "Ürün başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteProduct()
        {
            await _productWithProductBrandStaticRepository.DeleteAsync();
            TempData["Message"] = "Ürün başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
