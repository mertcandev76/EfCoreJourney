using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EfCoreJourney.Controllers
{
    public class OrderPaymentController : Controller
    {
        private readonly IOrderPaymentService _orderPaymentService;
        private readonly IGenericService<Order> _orderService;
        public OrderPaymentController(IOrderPaymentService orderPaymentService, IGenericService<Order> orderService)
        {
            _orderPaymentService = orderPaymentService;
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orderPayments = await _orderPaymentService.GetAllWithOrderAsync();
            return View(orderPayments);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Orders = new SelectList(await _orderService.GetAllAsync(), "OrderID", "Notes");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(OrderPayment orderPayment)
        {
            if (ModelState.IsValid)
            {
                await _orderPaymentService.AddAsync(orderPayment);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Orders = new SelectList(await _orderService.GetAllAsync(), "OrderID", "Notes");
            return View(orderPayment);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var orderPayments = await _orderPaymentService.GetByIdWithOrderAsync(id);
            if (orderPayments == null) return NotFound();
            ViewBag.Orders = new SelectList(await _orderService.GetAllAsync(), "OrderID", "Notes", orderPayments.OrderID);
            return View(orderPayments);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderPayment orderPayment)
        {
            if (ModelState.IsValid)
            {
                await _orderPaymentService.UpdateAsync(orderPayment);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Orders = new SelectList(await _orderService.GetAllAsync(), "OrderID", "Notes", orderPayment.OrderID);
            return View(orderPayment);
        }
        // GET: Silme onay sayfası
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var orderPayments = await _orderPaymentService.GetByIdWithOrderAsync(id);
            if (orderPayments == null) return NotFound();

            return View(orderPayments);
        }

        // POST: Silme işlemi
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderPayments = await _orderPaymentService.GetByIdAsync(id);
            if (orderPayments != null)
            {
                await _orderPaymentService.DeleteAsync(orderPayments);
            }

            return RedirectToAction(nameof(Index));
        }
        // Detaylar
        public async Task<IActionResult> Details(int id)
        {
            var orderPayments = await _orderPaymentService.GetByIdWithOrderAsync(id);
            if (orderPayments == null) return NotFound();

            return View(orderPayments);
        }
    }
}
