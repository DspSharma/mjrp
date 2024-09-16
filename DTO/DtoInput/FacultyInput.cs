namespace MJRPAdmin.DTO.DtoInput
{
    public class FacultyInput
    {
        public int Id { get; set; }
        public string Tittle { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
