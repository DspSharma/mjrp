using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using MJRPAdmin.DTO.DtoInput;
using MJRPAdmin.Models;
using MJRPAdmin.Service.interfaces;
using Newtonsoft.Json;
using NuGet.DependencyResolver;
using System;

namespace MJRPAdmin.Service
{
    public class LoginService:ILoginService
    {
       
        private   IWebHostEnvironment _environment;
        public IMapper _mapper;
        public LoginService(IWebHostEnvironment environment,IMapper mapper)
        {
            _environment = environment;
            _mapper = mapper;
        }

        public  List<User> getUserJson()
        {
            var rootPath = _environment.WebRootPath;
           
            var fullPath = Path.Combine(rootPath, "fileDb/User.json");


            using StreamReader reader = new(fullPath);
            var json = reader.ReadToEnd();
            List<User> users = JsonConvert.DeserializeObject<List<User>>(json);
            return users;
        }

        public async Task<ApiResponseModels<LoginInUserModels>> Login(UserLoginInput value)
        {
            var users = getUserJson();
            var usersJson = users.Where(x => x.Email == value.Email && x.Password == value.Password).FirstOrDefault();
            if (usersJson == null)
            {
                return new ApiResponseModels<LoginInUserModels>
                {
                    succeed = false,
                    message = "No User found"
                };
            }
            LoginInUserModels user = _mapper.Map<LoginInUserModels>(usersJson);
            var rslt = new ApiResponseModels<LoginInUserModels>
            {
                succeed = true,
                message = "success",
                data = user
            };
            return rslt;
        }


    }

}
