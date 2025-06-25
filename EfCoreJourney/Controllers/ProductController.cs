using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EfCoreJourney.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IGenericService<ProductBrand> _brandService;
        private readonly IGenericService<ProductCategory> _categoryService;
        public ProductController(IProductService productService, IGenericService<ProductBrand> brandService, IGenericService<ProductCategory> categoryService)
        {
            _productService = productService;
            _brandService = brandService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllWithBrandandCategoryAsync();
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Brands = new SelectList(await _brandService.GetAllAsync(), "ProductBrandID", "Name");
            ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "CategoryID", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Brands = new SelectList(await _brandService.GetAllAsync(), "ProductBrandID", "Name");
            ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "CategoryID", "Name");
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var products = await _productService.GetByIdWithBrandandCategoryAsync(id);
            if (products == null) return NotFound();
            ViewBag.Brands = new SelectList(await _brandService.GetAllAsync(), "ProductBrandID", "Name",products.ProductBrandID);
            ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "CategoryID", "Name",products.ProductCategoryID);
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateAsync(product);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Brands = new SelectList(await _brandService.GetAllAsync(), "ProductBrandID", "Name", product.ProductBrandID);
            ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "CategoryID", "Name", product.ProductCategoryID);
            return View(product);
        }
        // GET: Silme onay sayfası
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var products = await _productService.GetByIdWithBrandandCategoryAsync(id);
            if (products == null) return NotFound();

            return View(products);
        }

        // POST: Silme işlemi
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _productService.GetByIdAsync(id);
            if (products != null)
            {
                await _productService.DeleteAsync(products);
            }

            return RedirectToAction(nameof(Index));
        }
        // Detaylar
        public async Task<IActionResult> Details(int id)
        {
            var products = await _productService.GetByIdWithBrandandCategoryAsync(id);
            if (products == null) return NotFound();

            return View(products);
        }
            
    }
}
