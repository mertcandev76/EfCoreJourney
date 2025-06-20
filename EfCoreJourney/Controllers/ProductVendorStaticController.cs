using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class ProductVendorStaticController : Controller
    {
        private readonly IProductVendorStaticRepository _productsVendorStaticRepository;

        public ProductVendorStaticController(IProductVendorStaticRepository productsVendorStaticRepository)
        {
            _productsVendorStaticRepository = productsVendorStaticRepository;
        }

        public async Task<IActionResult> Index()
        {
            var productVendors = await _productsVendorStaticRepository.GetAllAsync();
            return View(productVendors);
        }
        public async Task<IActionResult> GetByID()
        {
            var productVendors = await _productsVendorStaticRepository.GetByIdAsync();
            if (productVendors == null)
                return NotFound("Belirtilen tedarikçi bulunamadı.");
            return View(productVendors);
        }
        public async Task<IActionResult> AddProductVendor()
        {
            await _productsVendorStaticRepository.AddAsync();
            TempData["Message"] = "Tedarikçi başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateProductVendor()
        {
            await _productsVendorStaticRepository.UpdateAsync();
            TempData["Message"] = "Tedarikçi başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteProductVendor()
        {
            await _productsVendorStaticRepository.DeleteAsync();
            TempData["Message"] = "Tedarikçi başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
