using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class OrderShipmentWithOrderStaticController : Controller
    {
        private readonly IOrderShipmentWithOrderStaticRepository _orderShipmentWithOrderStaticRepository;

        public OrderShipmentWithOrderStaticController(IOrderShipmentWithOrderStaticRepository orderShipmentWithOrderStaticRepository)
        {
            _orderShipmentWithOrderStaticRepository = orderShipmentWithOrderStaticRepository;
        }

        public async Task<IActionResult> Index()
        {
            var orderShipments = await _orderShipmentWithOrderStaticRepository.GetAllAsync();
            return View(orderShipments);
        }
        public async Task<IActionResult> GetByID()
        {
            var orderShipments= await _orderShipmentWithOrderStaticRepository.GetByIdAsync();
            if(orderShipments==null) return NotFound("Belirtilen ID'ye ait gönderim kaydı bulunamadı.");
            return View(orderShipments);
        }
        public async Task<IActionResult> AddOrderShipment()
        {

            await _orderShipmentWithOrderStaticRepository.AddAsync();
            TempData["Message"] = "Gönderim kaydı başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateOrderShipment()
        {
            await _orderShipmentWithOrderStaticRepository.UpdateAsync();
            TempData["Message"] = "Gönderim kaydı başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteOrderShipment()
        {
            await _orderShipmentWithOrderStaticRepository.DeleteAsync();
            TempData["Message"] = "Gönderim kaydı başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }


    }
}
