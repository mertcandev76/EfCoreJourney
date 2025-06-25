using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EfCoreJourney.Controllers
{
    public class StoreSettingController : Controller
    {
        private readonly IStoreSettingService _storeSettingService;
        private readonly IGenericService<Store> _storeService;

        public StoreSettingController(IStoreSettingService storeSettingService, IGenericService<Store> storeService)
        {
            _storeSettingService = storeSettingService;
            _storeService = storeService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var settings = await _storeSettingService.GetAllWithStoreAsync();
            return View(settings);
        }

        [HttpGet]
        // GET: Ekleme
        public async Task<IActionResult> Create()
        {
            ViewBag.Stores = new SelectList(await _storeService.GetAllAsync(), "StoreID", "Name");
            return View();
        }

        // POST: Ekleme
        [HttpPost]
        public async Task<IActionResult> Create(StoreSetting setting)
        {
            if (ModelState.IsValid)
            {
                await _storeSettingService.AddAsync(setting);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Stores = new SelectList(await _storeService.GetAllAsync(), "StoreID", "Name");
            return View(setting);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var setting = await _storeSettingService.GetByIdWithStoreAsync(id);
            if (setting == null) return NotFound();

            ViewBag.Stores = new SelectList(await _storeService.GetAllAsync(), "StoreID", "Name", setting.StoreId);
            return View(setting);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StoreSetting setting)
        {
            if (ModelState.IsValid)
            {
                await _storeSettingService.UpdateAsync(setting);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Stores = new SelectList(await _storeService.GetAllAsync(), "StoreID", "Name", setting.StoreId);
            return View(setting);
        }
        // GET: Silme onay sayfası
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var setting = await _storeSettingService.GetByIdWithStoreAsync(id);
            if (setting == null) return NotFound();

            return View(setting);
        }

        // POST: Silme işlemi
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var setting = await _storeSettingService.GetByIdAsync(id);
            if (setting != null)
            {
                await _storeSettingService.DeleteAsync(setting);
            }

            return RedirectToAction(nameof(Index));
        }

        // Detaylar
        public async Task<IActionResult> Details(int id)
        {
            var setting = await _storeSettingService.GetByIdWithStoreAsync(id);
            if (setting == null) return NotFound();

            return View(setting);
        }

    }
}
