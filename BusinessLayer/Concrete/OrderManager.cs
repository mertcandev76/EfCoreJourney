using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class OrderManager:GenericManager<Order>,IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderManager(IOrderRepository repository):base(repository)
        {
            _repository = repository;
        }

        public async Task<List<Order>> GetAllWithCustomerAsync() => await _repository.GetAllWithCustomerAsync();

        public async Task<Order?> GetByIdWithCustomerAsync(int id) => await _repository.GetByIdWithCustomerAsync(id);
    }
}
