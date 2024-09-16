using MJRPAdmin.DTO.DtoInput;
using MJRPAdmin.DTO.DtoOutPut;
using MJRPAdmin.Models;

namespace MJRPAdmin.Service.interfaces
{
    public interface IFacultyService
    {
        public  Task<ApiResponseModels<FacultyOutPut>> facultyAdd(FacultyInput value);
   
        public  Task<ApiResponseModels<bool>> facultyDeleteByID(int id);

        public  Task<ApiResponseModels<FacultyOutPut>> FacultyEditByID(int id);
        public  Task<ApiResponseModels<FacultyOutPut>> facultyUpdate(FacultyInput value);

        //  public  Task<List<result>> uploadExcelFile(UploadExcelInput value);
        public Task<ApiResponseModels<string>> readTextFile(string path);
        List<Faculty> getFacultyJson();

    }
}
