📊 Hiyerarşiye Göre Açıklama:

Customer (ANA TABLO)
  ├─ Order (CustomerID FK) → BAĞIMLI
  │   ├─ OrderDetail (OrderID FK) → BAĞIMLI
  │   │    └─ Product (ProductID = ANA) → ANA
  │   │         ├─ ProductBrand (ProductBrandID = ANA) → ANA
  │   │         └─ VendorProduct (ProductID, VendorID FK) → BAĞIMLI
  │   │              └─ Vendor (VendorID = ANA) → ANA
  │   ├─ OrderPayment (OrderID FK) → BAĞIMLI
  │   └─ OrderShipment (OrderID FK) → BAĞIMLI
  └─ CustomerCoupon (CustomerID, CouponID FK) → BAĞIMLI
        └─ Coupon (CouponID = ANA) → ANA

Store (ANA TABLO)
  └─ StoreSetting (StoreSettingID = PK, StoreID = FK) → BAĞIMLI


🔄 Özet Sıralama:

ProductBrand
ProductVendor
Product
VendorProduct
Coupon
Customer
CustomerCoupon
Order
OrderDetail
OrderPayment
OrderShipment
Store
StoreSetting

