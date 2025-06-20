using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class StoreWithStoreSettingStaticController : Controller
    {
        private readonly IStoreWithStoreSettingStaticRepository _storeWithStoreSettingStaticRepository;

        public StoreWithStoreSettingStaticController(IStoreWithStoreSettingStaticRepository storeWithStoreSettingStaticRepository)
        {
            _storeWithStoreSettingStaticRepository = storeWithStoreSettingStaticRepository;
        }


        //Listeleme
        public async Task<IActionResult> Index()
        {
            var stores= await _storeWithStoreSettingStaticRepository.GetAllAsync();
            return View(stores);
        }
        //ID Getirme İşlemi
        public async Task<IActionResult> GetByID()
        {
            var stores = await _storeWithStoreSettingStaticRepository.GetByIdAsync();
            if (stores==null)
            {
                return NotFound("Belirtilen ID'ye ait mağaza bulunamadı.");
            }
            return View(stores);
        }
        // Ekleme işlemi
        public async Task<IActionResult> AddStore()
        {
            await _storeWithStoreSettingStaticRepository.AddAsync();
            TempData["Message"] = "Mağaza başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }
        // Güncelleme işlemi
        public async Task<IActionResult> UpdateStore()
        {
            await _storeWithStoreSettingStaticRepository.UpdateAsync();
            TempData["Message"] = "Mağaza başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }
        // Ekleme işlemi
        public async Task<IActionResult> DeleteStore()
        {
            await _storeWithStoreSettingStaticRepository.DeleteAsync();
            TempData["Message"] = "Mağaza başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }

    }
}
