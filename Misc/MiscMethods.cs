using MJRPAdmin.Models;
using Newtonsoft.Json;

namespace MJRPAdmin.Misc
{
    public class MiscMethods
    {
        public static string uploadFileToLocal(IFormFile file, string dirpath)
        {
            string fileNameWitoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
            if (fileNameWitoutExtension.Length > 20)
                fileNameWitoutExtension = fileNameWitoutExtension.Substring(0, 20);

            string uniqueFileName = string.Concat(
                DateTime.Now.ToString("ddMMyyyyHHmmssfff"),
                fileNameWitoutExtension,
                Path.GetExtension(file.FileName)
                );
            string filepath = Path.Combine(dirpath, uniqueFileName);
            var fileStream = new FileStream(filepath, FileMode.Create);
            file.CopyTo(fileStream);
            fileStream.Dispose();
            return uniqueFileName;
            // return filepath;
        }

        public static void deleteFile(string dirpath)
        {
            if (File.Exists(dirpath))
                File.Delete(dirpath);
        }

        public static string getUniqueFileNameWithTimeStamp(string fileName, int saveLength = 20)
        {
            string fileNameWitoutExtension = Path.GetFileNameWithoutExtension(fileName);
            if (fileNameWitoutExtension.Length > saveLength)
                fileNameWitoutExtension = fileNameWitoutExtension.Substring(0, 20);

            return string.Concat(
                DateTime.Now.ToString("ddMMyyyyHHmmssfff"),
                fileNameWitoutExtension,
                Path.GetExtension(fileName)
                );
        }

    }
}
