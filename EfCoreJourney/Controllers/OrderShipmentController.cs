using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EfCoreJourney.Controllers
{
    public class OrderShipmentController : Controller
    {
        private readonly IOrderShipmentService _orderShipmentService;
        private readonly IGenericService<Order> _orderService;

        public OrderShipmentController(IOrderShipmentService orderShipmentService, IGenericService<Order> orderService)
        {
            _orderShipmentService = orderShipmentService;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orderShipments = await _orderShipmentService.GetAllWithOrderAsync();
            return View(orderShipments);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Orders = new SelectList(await _orderService.GetAllAsync(), "OrderID", "Notes");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(OrderShipment orderShipment)
        {
            if (ModelState.IsValid)
            {
                await _orderShipmentService.AddAsync(orderShipment);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Orders = new SelectList(await _orderService.GetAllAsync(), "OrderID", "Notes");
            return View(orderShipment);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var orderShipments = await _orderShipmentService.GetByIdWithOrderAsync(id);
            if (orderShipments == null) return NotFound();
            ViewBag.Orders = new SelectList(await _orderService.GetAllAsync(), "OrderID", "Notes",orderShipments.OrderID);
            return View(orderShipments);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderShipment orderShipment)
        {
            if (ModelState.IsValid)
            {
                await _orderShipmentService.UpdateAsync(orderShipment);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Orders = new SelectList(await _orderService.GetAllAsync(), "OrderID", "Notes", orderShipment.OrderID);
            return View(orderShipment);
        }
        // GET: Silme onay sayfası
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var orderShipments = await _orderShipmentService.GetByIdWithOrderAsync(id);
            if (orderShipments == null) return NotFound();

            return View(orderShipments);
        }

        // POST: Silme işlemi
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderShipments = await _orderShipmentService.GetByIdAsync(id);
            if (orderShipments != null)
            {
                await _orderShipmentService.DeleteAsync(orderShipments);
            }

            return RedirectToAction(nameof(Index));
        }
        // Detaylar
        public async Task<IActionResult> Details(int id)
        {
            var orderShipments = await _orderShipmentService.GetByIdWithOrderAsync(id);
            if (orderShipments == null) return NotFound();

            return View(orderShipments);
        }
    }
}
