namespace MJRPAdmin.Service.interfaces
{
    public interface IDocumentService
    {
        T readDocment<T>(string filePath);
        void writeDocument<T>(T data, string filePath);
    }
}
