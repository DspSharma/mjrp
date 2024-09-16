using AutoMapper;
using MJRPAdmin.DTO.DtoInput;
using MJRPAdmin.DTO.DtoOutPut;
using MJRPAdmin.Misc;
using MJRPAdmin.Models;
using MJRPAdmin.Service.interfaces;
using Newtonsoft.Json;

namespace MJRPAdmin.Service
{
    public class UserService : IUserService
    {
        private IWebHostEnvironment _environment;
        public IMapper _mapper;

        public UserService(IWebHostEnvironment environment, IMapper mapper)
        {
            _environment = environment;
            _mapper = mapper;
        }

        public List<User> getUserJson()
        {
            var rootPath = _environment.WebRootPath;
            var fullPath = Path.Combine(rootPath, "document/User.json");
            using StreamReader reader = new(fullPath);
            var json = reader.ReadToEnd();
            List<User> users = JsonConvert.DeserializeObject<List<User>>(json);
            return users;
        }

        public void WriteFacultyJson(List<User> data, string filePath)
        {
            string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);

            File.WriteAllText(filePath, jsonString);

        }

        public async Task<ApiResponseModels<UserOutPut>> userProfileEdit(int id)
        {

            var facultyData = getUserJson();
            User faculty = facultyData.Where(x => x.Id == id).FirstOrDefault();
            var rslt = _mapper.Map<UserOutPut>(faculty);
            return new ApiResponseModels<UserOutPut>
            {
                succeed = true,
                message = "success",
                data = rslt
            };
        }


        public async Task<ApiResponseModels<UserOutPut>> userAdd(UserInput value)
        {
            var rootPath = _environment.WebRootPath;
            var fullPath = Path.Combine(rootPath, "document/User.json");
            var getUser = getUserJson();
            int nextId = getUser.Count > 0 ? getUser.Max(x => x.Id) + 1 : 1;
            User formValue = _mapper.Map<User>(value);

            int duduplicateData = getUser.Where(x=>x.Email == formValue.Email || x.Mobile == formValue.Mobile).Count();
            if(duduplicateData > 0)
            {
                return new ApiResponseModels<UserOutPut>
                {
                    succeed = false,
                    message = "Email or Mobile is already exits",
                };
            }
            formValue.Id = nextId;
            formValue.CreatedAt = DateTime.UtcNow;
            formValue.UpdatedAt = DateTime.UtcNow;
            getUser.Add(formValue);
            WriteFacultyJson(getUser, fullPath);
            return new ApiResponseModels<UserOutPut>
            {
                succeed = true,
                message = "success"
            };
        }

        public async Task<ApiResponseModels<List<UserOutPut>>> userList()
        {
            var getUser = getUserJson();
            var rslt = _mapper.Map<List<UserOutPut>>(getUser);
            return new ApiResponseModels<List<UserOutPut>>
            {
                succeed = true,
                message = "success",
                data = rslt
            };
        }
       

        public async Task<ApiResponseModels<bool>> userDeleteById(int id)
        {
            var rootPath = _environment.WebRootPath;
            var fullPath = Path.Combine(rootPath, "document/User.json");

            var getUser = getUserJson();
            User user = getUser.Where(x => x.Id == id).FirstOrDefault();
            getUser.Remove(user);
            WriteFacultyJson(getUser, fullPath);
            return new ApiResponseModels<bool>
            {
                succeed = true,
                message = "success",
            };
        }

        public async Task<ApiResponseModels<UserOutPut>> userEditById(int id)
        {

            var getUser = getUserJson();
            User user = getUser.Where(x => x.Id == id).FirstOrDefault();
            var rslt = _mapper.Map<UserOutPut>(user);
            return new ApiResponseModels<UserOutPut>
            {
                succeed = true,
                message = "success",
                data = rslt
            };
        }

        public async Task<ApiResponseModels<UserOutPut>> userUpdate(UserInput value)
        {
            var rootPath = _environment.WebRootPath;
            var fullPath = Path.Combine(rootPath, "document/User.json");
            var getUser = getUserJson();
            User user = getUser.Where(x => x.Id == value.Id).FirstOrDefault();
            user.Name = value.Name;
            user.Email = value.Email;
            user.Mobile = value.Mobile;
            user.Password = value.Password;
            user.UpdatedAt = DateTime.UtcNow;

            // getFaculty.(faculty);
            WriteFacultyJson(getUser, fullPath);

            return new ApiResponseModels<UserOutPut>
            {
                succeed = true,
                message = "success"
            };
        }

    }
}
