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
    public class OrderPaymentManager:GenericManager<OrderPayment>,IOrderPaymentService
    {
        private readonly IOrderPaymentRepository _repository;

        public OrderPaymentManager(IOrderPaymentRepository repository):base(repository) 
        {
            _repository = repository;
        }

        public async Task<List<OrderPayment>> GetAllWithOrderAsync() => await _repository.GetAllWithOrderAsync();

        public async Task<OrderPayment?> GetByIdWithOrderAsync(int id) => await _repository.GetByIdWithOrderAsync(id);
    }
}
