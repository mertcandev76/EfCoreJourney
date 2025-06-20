using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //FLUENT API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         

            // 1. 1-1 (Çift Yönlü) Store ⟷ StoreSetting
            modelBuilder.Entity<Store>()
                .HasOne(s => s.StoreSetting)
                .WithOne(ss => ss.Store)
                .HasForeignKey<StoreSetting>(ss => ss.StoreId);

            // 2. 1-N (Çift Yönlü) Customer ⟷ Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerID);

            // 3. 1-N (Çift Yönlü) Order ⟷ OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            // 4. 1-N (Çift Yönlü) Product ⟷ OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductID);

            // 5. N-N (Çift Yönlü) Customer ⟷ Coupon (Ara tablo: CustomerCoupon)
            modelBuilder.Entity<CustomerCoupon>()
                .HasOne(cc => cc.Customer)
                .WithMany(c => c.CustomerCoupons)
                .HasForeignKey(cc => cc.CustomerID);

            modelBuilder.Entity<CustomerCoupon>()
                .HasOne(cc => cc.Coupon)
                .WithMany(c => c.CustomerCoupons)
                .HasForeignKey(cc => cc.CouponID);

            // 6. 1-1 (Tek Yönlü) Order ⟷ OrderPayment
            modelBuilder.Entity<Order>()
                .HasOne(o => o.OrderPayment)
                .WithOne()
                .HasForeignKey<OrderPayment>(op => op.OrderID);

            // 7. 1-1 (Tek Yönlü) Order ⟷ OrderShipment
            modelBuilder.Entity<Order>()
                .HasOne(o => o.OrderShipment)
                .WithOne()
                .HasForeignKey<OrderShipment>(os => os.OrderID);

            // 8. 1-N (Tek Yönlü) ProductBrand ⟷ Product
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductBrand)
                .WithMany(pb => pb.Products)
                .HasForeignKey(p => p.ProductBrandID);

            // 9. N-N (Tek Yönlü) Product ⟷ ProductVendor (Ara tablo: VendorProduct)
            modelBuilder.Entity<VendorProduct>()
                .HasOne(vp => vp.Product)
                .WithMany(p => p.VendorProducts)
                .HasForeignKey(vp => vp.ProductID);

            modelBuilder.Entity<VendorProduct>()
                .HasOne(vp => vp.ProductVendor)
                .WithMany() // Tek yönlü olduğu için ProductVendor içinde ICollection yok
                .HasForeignKey(vp => vp.ProductVendorID);

            // 10. Log (bağımsız tablo - yapılandırma gerekmez ama isteğe bağlıdır)
            modelBuilder.Entity<Log>().ToTable("Logs");
        }

        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerCoupon> CustomerCoupons { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Order>  Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderPayment> OrderPayments { get; set; }
        public DbSet<OrderShipment> OrderShipments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductVendor> ProductVendors { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreSetting> StoreSettings { get; set; }
       
        public DbSet<VendorProduct> VendorProducts { get; set; }


    }

}
