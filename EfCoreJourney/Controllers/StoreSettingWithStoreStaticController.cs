using DataAccessLayer.Abstract;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class StoreSettingWithStoreStaticController : Controller
    {
        private readonly IStoreSettingWithStoreStaticRepository _settingWithStoreStaticRepository;

        public StoreSettingWithStoreStaticController(IStoreSettingWithStoreStaticRepository settingWithStoreStaticRepository)
        {
            _settingWithStoreStaticRepository = settingWithStoreStaticRepository;
        }

        public async Task<IActionResult> Index()
        {
            var storeSettings = await _settingWithStoreStaticRepository.GetAllAsync();
            return View(storeSettings);
        }
        public async Task<IActionResult> GetByID()
        {
            var storeSettings = await _settingWithStoreStaticRepository.GetByIdAsync();
            if (storeSettings == null)
                return NotFound("Belirtilen ID'ye ait mağaza ayarı bulunamadı.");
            return View(storeSettings);
        }
        public async Task<IActionResult> AddStoreSetting()
        {
            await _settingWithStoreStaticRepository.AddAsync();
            TempData["Message"] = "Mağaza ayarı başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateStoreSetting()
        {
            await _settingWithStoreStaticRepository.UpdateAsync();
            TempData["Message"] = "Mağaza ayarı başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteStoreSetting()
        {
            await _settingWithStoreStaticRepository.DeleteAsync();
            TempData["Message"] = "Mağaza ayarı başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
