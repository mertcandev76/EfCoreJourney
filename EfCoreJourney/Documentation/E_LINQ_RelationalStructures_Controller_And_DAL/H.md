🧠 N-N (Many-to-Many) İlişki Nedir?
İki tablo arasında çoktan çoğa ilişki varsa, her iki tablo da birbirinden birden fazla kayıtla ilişkili olabilir.

🎯 Gerçek Hayat Örneği:

Öğrenciler - Dersler (Students - Courses)
Bir öğrenci birden fazla derse girebilir.
Bir ders birden fazla öğrenciye ait olabilir.

[Student]───(∞)────(∞)───[Course]
🏷️ 1. Yöntem: Default Convention (Varsayılan Kurallar)
➡️ EF Core 5+ ile artık join (ilişki) tablosu yazmadan otomatik olarak tanır.

Student.cs
public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }

    // Çoktan çoka ilişki
    public ICollection<Course> Courses { get; set; }
}

Course.cs
public class Course
{
    public int CourseId { get; set; }
    public string Title { get; set; }

    public ICollection<Student> Students { get; set; }
}
📌ForeignKey kullanmadık çünkü EF Core, arka planda otomatik olarak "StudentCourse" adında bir ilişki tablosu oluşturur.
Peki neden :

Student] ───(∞)───[StudentCourse]───(∞)───[Course]
StudentCourse = Ara tablo (Join Table)

Bu tabloda iki foreign key vardır:
StudentId → Student
CourseId → Course



