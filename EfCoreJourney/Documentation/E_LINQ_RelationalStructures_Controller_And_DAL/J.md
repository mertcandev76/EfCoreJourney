3. Yöntem: Fluent API ile Many-to-Many
Bu yöntemle hem doğrudan ilişki kurabiliriz, hem de özel ayarlarla (ek kolon, composite key) detaylı yapılandırabiliriz.

✅ a) Ara tablo olmadan (otomatik)
modelBuilder.Entity<Student>()
    .HasMany(s => s.Courses)
    .WithMany(c => c.Students)
    .UsingEntity(j => j.ToTable("StudentCourses"));
📌 EF Core kendi StudentCourses isimli ara tabloyu oluşturur.

✅ b) Ara tabloyla manuel (detaylı kontrol)
modelBuilder.Entity<StudentCourse>()
    .HasKey(sc => new { sc.StudentId, sc.CourseId }); // Composite Key

modelBuilder.Entity<StudentCourse>()
    .HasOne(sc => sc.Student)
    .WithMany(s => s.StudentCourses)
    .HasForeignKey(sc => sc.StudentId);

modelBuilder.Entity<StudentCourse>()
    .HasOne(sc => sc.Course)
    .WithMany(c => c.StudentCourses)
    .HasForeignKey(sc => sc.CourseId);