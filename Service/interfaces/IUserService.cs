using MJRPAdmin.DTO.DtoInput;
using MJRPAdmin.DTO.DtoOutPut;
using MJRPAdmin.Models;

namespace MJRPAdmin.Service.interfaces
{
    public interface IUserService
    {
        public Task<ApiResponseModels<UserOutPut>> userProfileEdit(int id);
        public  Task<ApiResponseModels<UserOutPut>> userAdd(UserInput value);

        public  Task<ApiResponseModels<List<UserOutPut>>> userList();

        public Task<ApiResponseModels<bool>> userDeleteById(int id);
        public  Task<ApiResponseModels<UserOutPut>> userEditById(int id);

        public  Task<ApiResponseModels<UserOutPut>> userUpdate(UserInput value);
    }
}
