using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EfCoreJourney.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IGenericService<Order> _orderService;
        private readonly IGenericService<Product> _productService;

        public OrderDetailController(IOrderDetailService orderDetailService, IGenericService<Order> orderService, IGenericService<Product> productService)
        {
            _orderDetailService = orderDetailService;
            _orderService = orderService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orderDetails = await _orderDetailService.GetAllWithOrderandProductAsync();
            return View(orderDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadSelectListsAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                await _orderDetailService.AddAsync(orderDetail);
                return RedirectToAction(nameof(Index));
            }

            await LoadSelectListsAsync();
            return View(orderDetail);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var orderDetails = await _orderDetailService.GetByIdWithOrderandProductAsync(id);
            if (orderDetails == null) return NotFound();

            await LoadSelectListsAsync(orderDetails.OrderId, orderDetails.ProductID);
            return View(orderDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                await _orderDetailService.UpdateAsync(orderDetail);
                return RedirectToAction(nameof(Index));
            }

            await LoadSelectListsAsync(orderDetail.OrderId, orderDetail.ProductID);
            return View(orderDetail);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var orderDetails = await _orderDetailService.GetByIdWithOrderandProductAsync(id);
            if (orderDetails == null) return NotFound();

            return View(orderDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDetails = await _orderDetailService.GetByIdAsync(id);
            if (orderDetails != null)
            {
                await _orderDetailService.DeleteAsync(orderDetails);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var orderDetails = await _orderDetailService.GetByIdWithOrderandProductAsync(id);
            if (orderDetails == null) return NotFound();

            return View(orderDetails);
        }

        // SelectList yükleme yardımcı metodu
        private async Task LoadSelectListsAsync(int? selectedOrderId = null, int? selectedProductId = null)
        {
            var orders = await _orderService.GetAllAsync();
            var products = await _productService.GetAllAsync();

            ViewBag.Orders = orders?
                .Select(o => new SelectListItem
                {
                    Value = o.OrderID.ToString(),
                    Text = o.OrderDate.ToString("dd.MM.yyyy") // formatlı tarih
                }).ToList();

            ViewBag.Products = products?
                .Select(p => new SelectListItem
                {
                    Value = p.ProductID.ToString(),
                    Text = $"{p.Name} - {p.Description}"
                }).ToList();
        }
    }
}
