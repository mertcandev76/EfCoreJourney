using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class StoreController : Controller
    {
        private readonly IGenericService<Store> _storeService;

        public StoreController(IGenericService<Store> storeService)
        {
            _storeService = storeService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var stories= await _storeService.GetAllAsync();
            return View(stories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Store store)
        {
           if (ModelState.IsValid)
            {
                await _storeService.AddAsync(store);
                return RedirectToAction(nameof(Index));
            }
            return View(store);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue) return RedirectToAction(nameof(Edit), new { id = 1 });
            var stories= await _storeService.GetByIdAsync(id.Value);
            if (stories == null) return NotFound("Hata");
            return View(stories);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Store store)
        {
            if (ModelState.IsValid)
            {

                await _storeService.UpdateAsync(store);
                return RedirectToAction(nameof(Index));
            }

            return View(store);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return RedirectToAction(nameof(Delete),new {id=1});
            var stories=  await _storeService.GetByIdAsync(id.Value);
            if (stories == null) return NotFound("Hata");
            return View(stories);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Store store)
        {
            if (ModelState.IsValid)
            {
                await _storeService.DeleteAsync(store);
                return RedirectToAction(nameof(Index));
            }
            return View(store);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue) return RedirectToAction(nameof(Details), new { id = 1 });
            var stories = await _storeService.GetByIdAsync(id.Value);
            if (stories == null) return NotFound("Hata");
            return View(stories);
        }
    }
}
