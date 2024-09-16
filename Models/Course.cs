namespace MJRPAdmin.Models
{
    public class Course
    {
        public int Id { get; set; }
        public int FacultyId { get; set; }
        public string CourseName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
