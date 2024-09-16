using MJRPAdmin.DTO.DtoInput;
using MJRPAdmin.Models;

namespace MJRPAdmin.Service.interfaces
{
    public interface ILoginService
    {
        public  Task<ApiResponseModels<LoginInUserModels>> Login(UserLoginInput value);
    }
}
