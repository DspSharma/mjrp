using MJRPAdmin.DTO.DtoInput;
using MJRPAdmin.DTO.DtoOutPut;
using MJRPAdmin.Entities;
using MJRPAdmin.Models;

namespace MJRPAdmin.Service.interfaces
{
    public interface IResultService
    {
        Task<ApiResponseModels<UploadExcelOutPut>> uploadResultByExcelFile(UploadExcelInput model);
        Task<ApiResponseModels<List<UploadResultOutput>>> getUploadExcelFile(int facultyId = 1);
        Task<ApiResponseModels<UploadResult>> deleteResult(int recId);
        Task<ApiResponseModels<UploadResult>> getResultDetail(int recId);
        Task<ApiResponseModels<UploadResult>> ShowHide(bool isVisible, int recId);
    }
}
