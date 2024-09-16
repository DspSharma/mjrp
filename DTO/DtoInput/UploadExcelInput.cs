namespace MJRPAdmin.DTO.DtoInput
{
    public class UploadExcelInput
    {
        public int Id { get; set; }
        public int FacultyId { get; set; }
        public int HeaderRow { get; set; }
        public int ResultRow { get; set; }
        public int RowGap { get; set; }
        public string RollNumberColumn { get; set; }
        public IFormFile? ExcleFile { get; set; }
        public string? ResultFile { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
