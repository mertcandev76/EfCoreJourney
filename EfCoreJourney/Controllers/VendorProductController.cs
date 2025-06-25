using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EfCoreJourney.Controllers
{
    public class VendorProductController : Controller
    {
        private readonly IVendorProductService _vendorProductService;
        private readonly IGenericService<Product> _productService;
        private readonly IGenericService<ProductVendor> _productVendorService;

        public VendorProductController(IVendorProductService vendorProductService, IGenericService<Product> productService, IGenericService<ProductVendor> productVendorService)
        {
            _vendorProductService = vendorProductService;
            _productService = productService;
            _productVendorService = productVendorService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vendorProducts = await _vendorProductService.GetAllWithProductandProductVendorAsync();
            return View(vendorProducts);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Products = new SelectList(await _productService.GetAllAsync(), "ProductID", "Name");
            ViewBag.ProductVendors = new SelectList(await _productVendorService.GetAllAsync(), "ProductVendorID", "CompanyName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(VendorProduct vendorProduct)
        {
            if (ModelState.IsValid)
            {
                await _vendorProductService.AddAsync(vendorProduct);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Products = new SelectList(await _productService.GetAllAsync(), "ProductID", "Name");
            ViewBag.ProductVendors = new SelectList(await _productVendorService.GetAllAsync(), "ProductVendorID", "CompanyName");
            return View(vendorProduct);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var vendorProducts = await _vendorProductService.GetByIdWithProductandProductVendorAsync(id);
            if (vendorProducts == null) return NotFound();
            ViewBag.Products = new SelectList(await _productService.GetAllAsync(), "ProductID", "Name", vendorProducts.ProductID);
            ViewBag.ProductVendors = new SelectList(await _productVendorService.GetAllAsync(), "ProductVendorID", "CompanyName", vendorProducts.ProductVendorID);
            return View(vendorProducts);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VendorProduct vendorProduct)
        {
            if (ModelState.IsValid)
            {
                await _vendorProductService.UpdateAsync(vendorProduct);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Products = new SelectList(await _productService.GetAllAsync(), "ProductID", "Name", vendorProduct.ProductID);
            ViewBag.ProductVendors = new SelectList(await _productVendorService.GetAllAsync(), "ProductVendorID", "CompanyName", vendorProduct.ProductVendorID);
            return View(vendorProduct);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var vendorProducts = await _vendorProductService.GetByIdWithProductandProductVendorAsync(id);
            if (vendorProducts == null) return NotFound();

            return View(vendorProducts);
        }

        // POST: Silme işlemi
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorProducts = await _vendorProductService.GetByIdAsync(id);
            if (vendorProducts != null)
            {
                await _vendorProductService.DeleteAsync(vendorProducts);
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            var vendorProducts = await _vendorProductService.GetByIdWithProductandProductVendorAsync(id);
            if (vendorProducts == null) return NotFound();

            return View(vendorProducts);
        }
    }
}
