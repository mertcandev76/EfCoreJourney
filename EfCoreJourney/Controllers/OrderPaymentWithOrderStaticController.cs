using DataAccessLayer.Abstract;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class OrderPaymentWithOrderStaticController : Controller
    {
        private readonly IOrderPaymentWithOrderStaticRepository _orderPaymentWithOrderStaticRepository;

        public OrderPaymentWithOrderStaticController(IOrderPaymentWithOrderStaticRepository orderPaymentWithOrderStaticRepository)
        {
            _orderPaymentWithOrderStaticRepository = orderPaymentWithOrderStaticRepository;
        }

        public async Task<IActionResult> Index()
        {
            var orderPayments = await _orderPaymentWithOrderStaticRepository.GetAllAsync();
            return View(orderPayments);
        }
        public async Task<IActionResult> GetByID()
        {
            var orderPayments = await _orderPaymentWithOrderStaticRepository.GetByIdAsync();
            if (orderPayments == null) return NotFound("Belirtilen ID'ye sahip ödeme kaydı bulunamadı.");
            return View(orderPayments);
        }
        public async Task<IActionResult> AddOrderPayment()
        {

            await _orderPaymentWithOrderStaticRepository.AddAsync();
            TempData["Message"] = "Ödeme kaydı başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateOrderPayment()
        {
            await _orderPaymentWithOrderStaticRepository.UpdateAsync();
            TempData["Message"] = "✏️ Ödeme kaydı başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteOrderPayment()
        {
            await _orderPaymentWithOrderStaticRepository.DeleteAsync();
            TempData["Message"] = "🗑️ Ödeme kaydı başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
