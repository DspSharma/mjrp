namespace MJRPAdmin.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Tittle { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
