using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductBrandConfiguration());
            modelBuilder.ApplyConfiguration(new ProductVendorConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new VendorProductConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new CouponConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerCouponConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderShipmentConfiguration());
            modelBuilder.ApplyConfiguration(new OrderPaymentConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new StoreConfiguration());
            modelBuilder.ApplyConfiguration(new StoreSettingConfiguration());
            modelBuilder.ApplyConfiguration(new LogConfiguration());
        }
        public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
        {
            public void Configure(EntityTypeBuilder<ProductCategory> builder)
            {
                builder.HasKey(pc => pc.ProductCategoryID);

                builder.Property(pc => pc.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(pc => pc.Description)
                    .IsRequired(false);

                builder.HasMany(pc => pc.Products)
                    .WithOne(p => p.Category)
                    .HasForeignKey(p => p.ProductCategoryID)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }
        public class ProductBrandConfiguration : IEntityTypeConfiguration<ProductBrand>
        {
            public void Configure(EntityTypeBuilder<ProductBrand> builder)
            {
                builder.HasKey(pb => pb.ProductBrandID);

                builder.Property(pb => pb.Name)
                    .HasMaxLength(100)
                    .IsRequired(false);

                builder.Property(pb => pb.Description)
                    .IsRequired(false);

                builder.Property(pb => pb.Country)
                    .HasMaxLength(50)
                    .IsRequired(false);

                builder.HasMany(pb => pb.Products)
                    .WithOne(p => p.ProductBrand)
                    .HasForeignKey(p => p.ProductBrandID)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }

        public class ProductVendorConfiguration : IEntityTypeConfiguration<ProductVendor>
        {
            public void Configure(EntityTypeBuilder<ProductVendor> builder)
            {
                builder.HasKey(pv => pv.ProductVendorID);

                builder.Property(pv => pv.CompanyName)
                    .HasMaxLength(200)
                    .IsRequired(false);

                builder.Property(pv => pv.ContactPerson)
                    .HasMaxLength(100)
                    .IsRequired(false);

                builder.Property(pv => pv.Email)
                    .HasMaxLength(100)
                    .IsRequired(false);

                builder.Property(pv => pv.Phone)
                    .HasMaxLength(20)
                    .IsRequired(false);

                builder.HasMany(pv => pv.VendorProducts)
                    .WithOne(vp => vp.ProductVendor)
                    .HasForeignKey(vp => vp.ProductVendorID)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }

        public class ProductConfiguration : IEntityTypeConfiguration<Product>
        {
            public void Configure(EntityTypeBuilder<Product> builder)
            {
                builder.HasKey(p => p.ProductID);

                builder.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(p => p.Description)
                    .IsRequired(false);

                builder.Property(p => p.Price)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                builder.Property(p => p.Stock)
                    .IsRequired();

                builder.Property(p => p.IsActive)
                    .IsRequired();

                builder.HasOne(p => p.ProductBrand)
                    .WithMany(pb => pb.Products)
                    .HasForeignKey(p => p.ProductBrandID)
                    .OnDelete(DeleteBehavior.Restrict);

                builder.HasOne(p => p.Category)
                    .WithMany(pc => pc.Products)
                    .HasForeignKey(p => p.ProductCategoryID)
                    .OnDelete(DeleteBehavior.Restrict);

                builder.HasMany(p => p.OrderDetails)
                    .WithOne(od => od.Product)
                    .HasForeignKey(od => od.ProductID)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasMany(p => p.VendorProducts)
                    .WithOne(vp => vp.Product)
                    .HasForeignKey(vp => vp.ProductID)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }

        public class VendorProductConfiguration : IEntityTypeConfiguration<VendorProduct>
        {
            public void Configure(EntityTypeBuilder<VendorProduct> builder)
            {
                builder.HasKey(vp => vp.VendorProductID);

                builder.Property(vp => vp.SupplyPrice)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                builder.Property(vp => vp.SuppliedDate)
                    .IsRequired(false);

                builder.HasOne(vp => vp.Product)
                    .WithMany(p => p.VendorProducts)
                    .HasForeignKey(vp => vp.ProductID)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(vp => vp.ProductVendor)
                    .WithMany(pv => pv.VendorProducts)
                    .HasForeignKey(vp => vp.ProductVendorID)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }

        public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
        {
            public void Configure(EntityTypeBuilder<Customer> builder)
            {
                builder.HasKey(c => c.CustomerID);

                builder.Property(c => c.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(c => c.Age)
                    .IsRequired(false);

                builder.Property(c => c.Email)
                    .HasMaxLength(100)
                    .IsRequired(false);

                builder.Property(c => c.Phone)
                    .HasMaxLength(20)
                    .IsRequired(false);

                builder.Property(c => c.Address)
                    .HasMaxLength(250)
                    .IsRequired(false);

                builder.Property(c => c.IsActive)
                    .IsRequired(false);

                builder.Property(c => c.BirthDate)
                    .IsRequired(false);

                builder.HasMany(c => c.Orders)
                    .WithOne(o => o.Customer)
                    .HasForeignKey(o => o.CustomerID)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasMany(c => c.CustomerCoupons)
                    .WithOne(cc => cc.Customer)
                    .HasForeignKey(cc => cc.CustomerID)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }

        public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
        {
            public void Configure(EntityTypeBuilder<Coupon> builder)
            {
                builder.HasKey(c => c.CouponID);

                builder.Property(c => c.Code)
                    .HasMaxLength(50)
                    .IsRequired(false);

                builder.Property(c => c.DiscountRate)
                    .HasColumnType("decimal(5,2)")
                    .IsRequired();

                builder.Property(c => c.ValidFrom)
                    .IsRequired();

                builder.Property(c => c.ValidUntil)
                    .IsRequired();

                builder.Property(c => c.IsActive)
                    .IsRequired();

                builder.HasMany(c => c.CustomerCoupons)
                    .WithOne(cc => cc.Coupon)
                    .HasForeignKey(cc => cc.CouponID)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }

        public class CustomerCouponConfiguration : IEntityTypeConfiguration<CustomerCoupon>
        {
            public void Configure(EntityTypeBuilder<CustomerCoupon> builder)
            {
                builder.HasKey(cc => cc.CustomerCouponID);

                builder.Property(cc => cc.RedeemedAt)
                    .IsRequired(false);

                builder.HasOne(cc => cc.Customer)
                    .WithMany(c => c.CustomerCoupons)
                    .HasForeignKey(cc => cc.CustomerID)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(cc => cc.Coupon)
                    .WithMany(c => c.CustomerCoupons)
                    .HasForeignKey(cc => cc.CouponID)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }

        public class OrderConfiguration : IEntityTypeConfiguration<Order>
        {
            public void Configure(EntityTypeBuilder<Order> builder)
            {
                builder.HasKey(o => o.OrderID);

                builder.Property(o => o.OrderDate)
                    .IsRequired();

                builder.Property(o => o.TotalAmount)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired(false);

                builder.Property(o => o.Status)
                    .HasMaxLength(50)
                    .IsRequired(false);

                builder.Property(o => o.Notes)
                    .HasMaxLength(500)
                    .IsRequired(false);

                builder.HasOne(o => o.Customer)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(o => o.CustomerID)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasMany(o => o.OrderDetails)
                    .WithOne(od => od.Order)
                    .HasForeignKey(od => od.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(o => o.OrderPayment)
                    .WithOne(op => op.Order)
                    .HasForeignKey<OrderPayment>(op => op.OrderID)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(o => o.OrderShipment)
                    .WithOne(os => os.Order)
                    .HasForeignKey<OrderShipment>(os => os.OrderID)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }

        public class OrderShipmentConfiguration : IEntityTypeConfiguration<OrderShipment>
        {
            public void Configure(EntityTypeBuilder<OrderShipment> builder)
            {
                builder.HasKey(os => os.OrderShipmentID);

                builder.Property(os => os.Carrier)
                    .HasMaxLength(100)
                    .IsRequired(false);

                builder.Property(os => os.TrackingNumber)
                    .HasMaxLength(100)
                    .IsRequired(false);

                builder.Property(os => os.ShippedDate)
                    .IsRequired(false);

                builder.Property(os => os.DeliveredDate)
                    .IsRequired(false);

                builder.HasOne(os => os.Order)
                    .WithOne(o => o.OrderShipment)
                    .HasForeignKey<OrderShipment>(os => os.OrderID)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }

        public class OrderPaymentConfiguration : IEntityTypeConfiguration<OrderPayment>
        {
            public void Configure(EntityTypeBuilder<OrderPayment> builder)
            {
                builder.HasKey(op => op.OrderPaymentID);

                builder.Property(op => op.Amount)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                builder.Property(op => op.PaymentMethod)
                    .HasMaxLength(100)
                    .IsRequired(false);

                builder.Property(op => op.PaymentDate)
                    .IsRequired(false);

                builder.HasOne(op => op.Order)
                    .WithOne(o => o.OrderPayment)
                    .HasForeignKey<OrderPayment>(op => op.OrderID)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }

        public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
        {
            public void Configure(EntityTypeBuilder<OrderDetail> builder)
            {
                builder.HasKey(od => od.OrderDetailID);

                builder.Property(od => od.Quantity)
                    .IsRequired();

                builder.Property(od => od.UnitPrice)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                builder.Property(od => od.Discount)
                    .HasColumnType("decimal(5,2)")
                    .IsRequired(false);

                builder.Property(od => od.Notes)
                    .HasMaxLength(500)
                    .IsRequired(false);

                builder.HasOne(od => od.Order)
                    .WithMany(o => o.OrderDetails)
                    .HasForeignKey(od => od.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(od => od.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(od => od.ProductID)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }

        public class StoreConfiguration : IEntityTypeConfiguration<Store>
        {
            public void Configure(EntityTypeBuilder<Store> builder)
            {
                builder.HasKey(s => s.StoreID);

                builder.Property(s => s.Name)
                    .HasMaxLength(200)
                    .IsRequired(false);

                builder.Property(s => s.OwnerName)
                    .HasMaxLength(100)
                    .IsRequired(false);

                builder.Property(s => s.Email)
                    .HasMaxLength(100)
                    .IsRequired(false);

                builder.HasOne(s => s.StoreSetting)
                    .WithOne(ss => ss.Store)
                    .HasForeignKey<StoreSetting>(ss => ss.StoreId)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }

        public class StoreSettingConfiguration : IEntityTypeConfiguration<StoreSetting>
        {
            public void Configure(EntityTypeBuilder<StoreSetting> builder)
            {
                builder.HasKey(ss => ss.StoreSettingID);

                builder.Property(ss => ss.Currency)
                    .HasMaxLength(10)
                    .IsRequired(false);

                builder.Property(ss => ss.Language)
                    .HasMaxLength(10)
                    .IsRequired(false);

                builder.Property(ss => ss.EnableNotifications)
                    .IsRequired();

                builder.HasOne(ss => ss.Store)
                    .WithOne(s => s.StoreSetting)
                    .HasForeignKey<StoreSetting>(ss => ss.StoreId)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }

        public class LogConfiguration : IEntityTypeConfiguration<Log>
        {
            public void Configure(EntityTypeBuilder<Log> builder)
            {
                builder.HasKey(l => l.LogID);

                builder.Property(l => l.LogDate)
                    .IsRequired(false);

                builder.Property(l => l.LogLevel)
                    .HasMaxLength(100)
                    .IsRequired(false);

                builder.Property(l => l.Message)
                    .IsRequired(false);

                builder.Property(l => l.Details)
                    .IsRequired(false);

                builder.Property(l => l.Source)
                    .HasMaxLength(100)
                    .IsRequired(false);

                builder.Property(l => l.User)
                    .HasMaxLength(100)
                    .IsRequired(false);
            }
        }



        public DbSet<ProductCategory> Categories { get; set; }
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
