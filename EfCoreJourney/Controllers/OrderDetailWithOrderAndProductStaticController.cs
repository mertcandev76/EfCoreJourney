using DataAccessLayer.Abstract;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class OrderDetailWithOrderAndProductStaticController : Controller
    {
        private readonly IOrderDetailWithOrderAndProductStaticRepository _orderDetailsStaticRepository;

        public OrderDetailWithOrderAndProductStaticController(IOrderDetailWithOrderAndProductStaticRepository orderDetailsStaticRepository)
        {
            _orderDetailsStaticRepository = orderDetailsStaticRepository;
        }

        public async Task<IActionResult> Index()
        {
            var orderDetails = await _orderDetailsStaticRepository.GetAllAsync();
            return View(orderDetails);
        }
        public async Task<IActionResult> GetByID()
        {
            var orderDetails = await _orderDetailsStaticRepository.GetByIdAsync();
            if (orderDetails == null) return NotFound(" Sabit ID'li sipariş bulunamadı.");
            return View(orderDetails);
        }
        public async Task<IActionResult> AddOrderDetail()
        {
            await _orderDetailsStaticRepository.AddAsync();
            TempData["Message"] = " Yeni sipariş detayı başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateOrderDetail()
        {
            await _orderDetailsStaticRepository.UpdateAsync();
            TempData["Message"] = " Sipariş detayı başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteOrderDetail()
        {
            await _orderDetailsStaticRepository.DeleteAsync();
            TempData["Message"] = " Sipariş detayı başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
