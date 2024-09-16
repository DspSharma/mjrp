using MJRPAdmin.Entities;

namespace MJRPAdmin.DTO.DtoOutPut
{
    public class CourseOutput
    {
        //public int Id { get; set; }
        public int FacultyId { get; set; }
        //public int ResultId { get; set; }
        //public string FacultyName { get; set; }
        public List<UploadResult> UploadResult { get; set; }
        //public bool IsActive { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; }
    }
}
