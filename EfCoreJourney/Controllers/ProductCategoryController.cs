using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly IGenericService<ProductCategory> _categoryService;

        public ProductCategoryController(IGenericService<ProductCategory> categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCategory category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var categories = await _categoryService.GetByIdAsync(id);
            if (categories == null) return NotFound();
            return View(categories);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductCategory category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var categories = await _categoryService.GetByIdAsync(id);
            if (categories == null) return NotFound();
            return View(categories);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categories = await _categoryService.GetByIdAsync(id);
            if (categories != null) await _categoryService.DeleteAsync(categories);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Details(int id)
        {
            var categories = await _categoryService.GetByIdAsync(id);
            if (categories == null) return NotFound();
            return View(categories);
        }
    }
}
