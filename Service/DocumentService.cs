using MJRPAdmin.Models;
using MJRPAdmin.Service.interfaces;
using Newtonsoft.Json;

namespace MJRPAdmin.Service
{
    public class DocumentService : IDocumentService
    {
        private IWebHostEnvironment _environment;
        public DocumentService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public T readDocment<T>(string filePath)
        {
            var rootPath = _environment.WebRootPath;
            var fullPath = Path.Combine(rootPath, filePath);
            using StreamReader reader = new(fullPath);
            string json = reader.ReadToEnd();
            T res = JsonConvert.DeserializeObject<T>(json);
            return res;
        }

        public void writeDocument<T>(T data, string filePath)
        {
            var rootPath = _environment.WebRootPath;
            var fullPath = Path.Combine(rootPath, filePath);
            string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);

            File.WriteAllText(fullPath, jsonString);

        }
    }
}
