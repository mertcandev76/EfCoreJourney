using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class LogController : Controller
    {
        private readonly IGenericService<Log> _logService;

        public LogController(IGenericService<Log> logService)
        {
            _logService = logService;
        }



        // Listeleme
        public async Task<IActionResult> Index()
        {
            var logs = await _logService.GetAllAsync();
            return View(logs);
        }

        // Ekleme - GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Ekleme - POST
        [HttpPost]
        public async Task<IActionResult> Create(Log log)
        {
            if (ModelState.IsValid)
            {
                await _logService.AddAsync(log);
                return RedirectToAction(nameof(Index));
            }
            return View(log);
        }
        // Güncelleme - GET
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                //sabit bir id ile yönlendir
                return RedirectToAction("Edit", new { id = 1 });
            }

            var logs = await _logService.GetByIdAsync(id.Value);
            if (logs == null)
                return NotFound();

            return View(logs);
        }

        // Güncelleme - POST
        [HttpPost]
        public async Task<IActionResult> Edit(Log log)
        {
            if (ModelState.IsValid)
            {
                await _logService.UpdateAsync(log);
                return RedirectToAction(nameof(Index));
            }
            return View(log);
        }

        // Silme - GET (onay ekranı için)
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var log = await _logService.GetByIdAsync(id);
            if (log == null) return NotFound();
            return View(log);
        }

        // Silme - POST
        [HttpPost]
        public async Task<IActionResult> Delete(Log log)
        {
     

            await _logService.DeleteAsync(log);
            return RedirectToAction("Index");
        }

        // Detay
        public async Task<IActionResult> Details(int id)
        {
            var log = await _logService.GetByIdAsync(id);
            if (log == null) return NotFound();
            return View(log);
        }

    }
}
