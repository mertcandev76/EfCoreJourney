Migration içindeki Up() ve Down() metotları nedir, ne işe yarar?

Entity Framework Core'da bir migration oluşturduğunda, arka planda otomatik olarak bir C# sınıfı üretilir. Bu sınıf içinde 2 temel metod vardır:

🔼 Up() Metodu (İleri Gidiş)
Yeni bir migration uygulandığında çalışır.

Veritabanına ne ekleneceğini tanımlar.

Tablolar, kolonlar, ilişkiler vs. burada oluşturulur.

Örnek:
protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.CreateTable(
        name: "Customers",
        columns: table => new
        {
            Id = table.Column<int>(nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"),
            Name = table.Column<string>(nullable: true)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Customers", x => x.Id);
        });
}
Bu kod, Customers adında bir tablo oluşturur.

🔽 Down() Metodu (Geri Dönüş)
Migration geri alınırken çalışır.

Up() metoduyla yapılan değişikliklerin tersini yapar.
Genelde DropTable, DropColumn, DropForeignKey gibi işlemler olur.


protected override void Down(MigrationBuilder migrationBuilder)
{
    migrationBuilder.DropTable(
        name: "Customers");
}
Bu da Customers tablosunu siler.