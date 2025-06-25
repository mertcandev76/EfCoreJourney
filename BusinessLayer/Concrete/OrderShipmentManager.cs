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
    public class OrderShipmentManager:GenericManager<OrderShipment>,IOrderShipmentService
    {
        private readonly IOrderShipmentRepository _repository;

        public OrderShipmentManager(IOrderShipmentRepository repository):base(repository) 
        {
            _repository = repository;
        }

        public async Task<List<OrderShipment>> GetAllWithOrderAsync() => await _repository.GetAllWithOrderAsync();

        public async Task<OrderShipment?> GetByIdWithOrderAsync(int id) => await _repository.GetByIdWithOrderAsync(id);
    }
}
