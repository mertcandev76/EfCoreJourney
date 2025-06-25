📊 Hiyerarşiye Göre Açıklama:

ProductCategory (1) ----- (N) Product (N) ----- (1) ProductBrand
                             |
                             | (N)
                             |
                         VendorProduct (N) ----- (1) ProductVendor

Customer (1) ----- (N) Order (1) ----- (N) OrderDetail (N) ----- (1) Product
        |                   |                | 
        |                   |                |
        |                   |          (1) OrderPayment
        |                   |          (1) OrderShipment
        |
        | (N)
        |
    CustomerCoupon (N) ----- (1) Coupon

Store (1) ----- (1) StoreSetting

Log (bağımsız tablo, ilişkisiz)




Açıklama:
ProductCategory ve Product bire çok ilişki (1 kategori, çok ürün)

ProductBrand ve Product bire çok (1 marka, çok ürün)

Product ve VendorProduct bire çok (1 ürün, çok VendorProduct kaydı)

ProductVendor ve VendorProduct bire çok (1 vendor, çok vendorproduct)

Customer ve Order bire çok (1 müşteri, çok sipariş)

Order ve OrderDetail bire çok (1 sipariş, çok sipariş detayı)

OrderDetail ile Product bire çok (1 ürün, çok sipariş detayı)

Order ve OrderPayment bire bir (1 sipariş, 1 ödeme)

Order ve OrderShipment bire bir (1 sipariş, 1 gönderim)

Customer ve CustomerCoupon bire çok (1 müşteri, çok müşteri kuponu)

CustomerCoupon ve Coupon bire çok (1 kupon, çok müşteri kuponu)

Store ve StoreSetting bire bir (1 mağaza, 1 ayar)

Log tablo bağımsız, ilişkisiz.