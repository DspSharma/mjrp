namespace MJRPAdmin.Models
{
    public class ApiResponseModels<T>
    {
        public bool succeed { get; set; }
        public string message { get; set; }

        public T data { get; set; }
        public string error { get; set; }
       
    }
}
