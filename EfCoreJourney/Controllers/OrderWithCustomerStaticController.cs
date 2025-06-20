using DataAccessLayer.Abstract;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class OrderWithCustomerStaticController : Controller
    {
        private readonly IOrderWithCustomerStaticRepository _orderStaticRepository;

        public OrderWithCustomerStaticController(IOrderWithCustomerStaticRepository orderStaticRepository)
        {
            _orderStaticRepository = orderStaticRepository;
        }

        //Listeleme İşlemi
        public async Task<IActionResult> Index()
        {
            var orders = await _orderStaticRepository.GetAllAsync();
            return View(orders);
        }
        //ID Getirme İşlemi
        public async Task<IActionResult> GetByID()
        {
            var orders = await _orderStaticRepository.GetByIdAsync();
            if (orders == null) return NotFound("Belirtilen ID ile eşleşen sipariş bulunamadı.");
            return View(orders);
        }
        //Ekleme İşlemi
        public async Task<IActionResult> AddOrder()
        {
            await _orderStaticRepository.AddAsync();
            TempData["Message"] = "Sipariş başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }
        //Güncelleme İşlemi
        public async Task<IActionResult> UpdateOrder()
        {
            await _orderStaticRepository.UpdateAsync();
            TempData["Message"] = "Sipariş başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }
        //Silme İşlemi
        public async Task<IActionResult> DeleteOrder()
        {
            await _orderStaticRepository.DeleteAsync();
            TempData["Message"] = "Sipariş başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
