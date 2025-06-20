using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class LogStaticController : Controller
    {
        private readonly ILogStaticRepository _logStaticRepository;

        public LogStaticController(ILogStaticRepository logStaticRepository)
        {
            _logStaticRepository = logStaticRepository;
        }

        //Listeleme İşlemi
        public async Task<IActionResult> Index()
        {
            var logs = await _logStaticRepository.GetAllAsync();
            return View(logs);
        }
        //ID Getirme İşlemi
        public async Task<IActionResult> GetByID()
        {
            var logs= await _logStaticRepository.GetByIdAsync();
            if (logs==null)
            {
                return NotFound("Belirtilen ID'ye ait herhangi bir log kaydı bulunamadı.");
            }
            return View(logs);
        }
        //Ekleme İşlemi
        public async Task<IActionResult> AddLog()
        {
            await _logStaticRepository.AddAsync();
            TempData["Message"] = "Log kaydı başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }
        //Güncelleme İşlemi
        public async Task<IActionResult> UpdateLog()
        {
            await _logStaticRepository.UpdateAsync();
            TempData["Message"] = "Log kaydı başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }
        //Silme İşlemi
        public async Task<IActionResult> DeleteLog()
        {
            await _logStaticRepository.DeleteAsync();
            TempData["Message"] = "Log kaydı başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
