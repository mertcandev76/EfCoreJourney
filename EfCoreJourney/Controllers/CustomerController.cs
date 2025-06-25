using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IGenericService<Customer> _customerService;

        public CustomerController(IGenericService<Customer> customerService)
        {
            _customerService = customerService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var customers= await _customerService.GetAllAsync();
            return View(customers);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerService.AddAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var customers = await _customerService.GetByIdAsync(id);
            if (customers == null) return NotFound();
            return View(customers);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerService.UpdateAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var customers = await _customerService.GetByIdAsync(id);
            if (customers == null) return NotFound();
            return View(customers);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customers = await _customerService.GetByIdAsync(id);
            if (customers != null) await _customerService.DeleteAsync(customers);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Details(int id)
        {
            var customers = await _customerService.GetByIdAsync(id);
            if (customers == null) return NotFound();
            return View(customers);
        }
    }
}
