using DataAccessLayer.Abstract;
using DataAccessLayer.Repositories;
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
            var logs= await _logStaticRepository.GetAllStaticLogsAsync();
            return View(logs);
        }

        //Static ID Getirme İşlemi
        public async Task<IActionResult> StaticLog()
        {
            var logs= await _logStaticRepository.GetStaticLogByIdAsync();
            if (logs == null)
            {
                return NotFound();
            }
            return View(logs);
        }

        //Static Ekleme İşlemi

        public async Task<IActionResult> AddStaticLog() 
        {
            await _logStaticRepository.AddStaticLogAsync();
            TempData["message"] = "Static Log Başarıyla Eklendi";
            return RedirectToAction(nameof(Index));
        }

        // Static güncelleme İşlemi
        public async Task<IActionResult> UpdateStaticLog()
        {
            await _logStaticRepository.UpdateStaticLogAsync();
            TempData["Message"] = "Statik Log Başarıyla Güncellendi.";
            return RedirectToAction(nameof(Index)); 
        }

        //Static Silme İşlemi
        public async Task<IActionResult> DeleteStaticLog()
        {
            await _logStaticRepository.DeleteStaticLogAsync();
            return RedirectToAction(nameof(Index)); 
        }
    }
}
