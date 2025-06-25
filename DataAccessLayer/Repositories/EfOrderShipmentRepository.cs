using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class EfOrderShipmentRepository : GenericRepository<OrderShipment>, IOrderShipmentRepository
    {
        public EfOrderShipmentRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<OrderShipment>> GetAllWithOrderAsync()
        {
            return await Table
                .Include(oS=>oS.Order)
                .ToListAsync();
        }

        public async Task<OrderShipment?> GetByIdWithOrderAsync(int id)
        {
            return await Table
                .Include(oS => oS.Order)
                .FirstOrDefaultAsync(oS=>oS.OrderShipmentID==id);
        }
    }
}
