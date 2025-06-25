using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static NuGet.Packaging.PackagingConstants;

namespace EfCoreJourney.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IGenericService<Customer> _customerService;

        public OrderController(IOrderService orderService, IGenericService<Customer> customerService)
        {
            _orderService = orderService;
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllWithCustomerAsync();
            return View(orders);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Customers = new SelectList(await _customerService.GetAllAsync(), "CustomerID", "FullName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                await _orderService.AddAsync(order);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Customers = new SelectList(await _customerService.GetAllAsync(), "CustomerID", "FullName");
            return View(order);
        }
         [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var orders = await _orderService.GetByIdWithCustomerAsync(id);
            if (orders == null) return NotFound();
            ViewBag.Customerss = new SelectList(await _customerService.GetAllAsync(), "CustomerID", "FullName", orders.CustomerID);
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                await _orderService.UpdateAsync(order);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Customerss = new SelectList(await _customerService.GetAllAsync(), "CustomerID", "FullName", order.CustomerID);
            return View(order);
        }
        // GET: Silme onay sayfası
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var orders = await _orderService.GetByIdWithCustomerAsync(id);
            if (orders == null) return NotFound();

            return View(orders);
        }

        // POST: Silme işlemi
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _orderService.GetByIdAsync(id);
            if (orders != null)
            {
                await _orderService.DeleteAsync(orders);
            }

            return RedirectToAction(nameof(Index));
        }
        // Detaylar
        public async Task<IActionResult> Details(int id)
        {
            var orders = await _orderService.GetByIdWithCustomerAsync(id);
            if (orders == null) return NotFound();

            return View(orders);
        }
    }
}
