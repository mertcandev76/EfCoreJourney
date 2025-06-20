using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class VendorProductWithProductAndProductVendorStaticController : Controller
    {
        private readonly IVendorProductWithProductAndProductVendorStaticRepository _vendorProductWithProductAndProductVendorStaticRepository;

        public VendorProductWithProductAndProductVendorStaticController(IVendorProductWithProductAndProductVendorStaticRepository vendorProductWithProductAndProductVendorStaticRepository)
        {
            _vendorProductWithProductAndProductVendorStaticRepository = vendorProductWithProductAndProductVendorStaticRepository;
        }

        public async Task<IActionResult> Index()
        {
            var vendorProducts= await _vendorProductWithProductAndProductVendorStaticRepository.GetAllAsync();
            return View(vendorProducts);
        }
        public async Task<IActionResult> GetById()
        {
            var vendorProducts = await _vendorProductWithProductAndProductVendorStaticRepository.GetByIdAsync();
            if(vendorProducts==null)
                return NotFound("Tedarikçi ürünü bulunamadı.");
            return View(vendorProducts);
        }
        public async Task<IActionResult> AddVendorProduct()
        {
            await _vendorProductWithProductAndProductVendorStaticRepository.AddAsync();
            TempData["Message"] = "Tedarikçi ürünü başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateVendorProduct()
        {
            await _vendorProductWithProductAndProductVendorStaticRepository.UpdateAsync();
            TempData["Message"] = "Tedarikçi ürünü başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteVendorProduct()
        {
            await _vendorProductWithProductAndProductVendorStaticRepository.DeleteAsync();
            TempData["Message"] = "Tedarikçi ürünü başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
