namespace MJRPAdmin.DTO.DtoOutPut
{
    public class UploadResultOutput
    {
        public int RecId { get; set; }
        public int? CollegeId { get; set; }
        public int? FacultyId { get; set; }
        public string? FacultyName { get; set; }
        public string? ResultDescription { get; set; }
        public string? FileName { get; set; }
        public DateTime? ResultDate { get; set; }
        public int? DisplayPriority { get; set; }
        public bool? IsVisible { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? NoOfRowsToDisplay { get; set; }
        public bool? IsNewFormat { get; set; }
    }
}
