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
    public class OrderDetailManager:GenericManager<OrderDetail>,IOrderDetailService
    {
        private readonly IOrderDetailRepository _repository;

        public OrderDetailManager(IOrderDetailRepository repository):base(repository)
        {
            _repository = repository;
        }

        public async Task<List<OrderDetail>> GetAllWithOrderandProductAsync() => await _repository.GetAllWithOrderandProductAsync();

        public async Task<OrderDetail?> GetByIdWithOrderandProductAsync(int id) => await _repository.GetByIdWithOrderandProductAsync(id);
    }
}
