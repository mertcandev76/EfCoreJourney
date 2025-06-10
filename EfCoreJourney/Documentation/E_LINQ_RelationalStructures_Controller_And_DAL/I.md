🏷️ 2. Yöntem: Data Annotations (Bu ilişki türü için sınırlı destek)

NOT!!!
Data Annotation yöntemi, N-N ilişkiler için doğrudan desteklemez.
Ama araya manuel bir ara tablo (join table) eklersen, yine kullanabilirsin.

Student.cs
public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }

    public ICollection<StudentCourse> StudentCourses { get; set; }
}
🔹 Course.cs
public class Course
{
    public int CourseId { get; set; }
    public string Title { get; set; }

    public ICollection<StudentCourse> StudentCourses { get; set; }
}

StudentCourse.cs
public class StudentCourse
{
    [Key]
    public int Id { get; set; } // Ya da composite key Fluent ile verilir

    [ForeignKey("Student")]
    public int StudentId { get; set; }

    [ForeignKey("Course")]
    public int CourseId { get; set; }

    public Student Student { get; set; }
    public Course Course { get; set; }
}