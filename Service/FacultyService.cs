using AutoMapper;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Hosting;
using MJRPAdmin.DTO.DtoInput;
using MJRPAdmin.DTO.DtoOutPut;
using MJRPAdmin.Misc;
using MJRPAdmin.Models;
using MJRPAdmin.Service.interfaces;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.DataValidation;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace MJRPAdmin.Service
{
    public class FacultyService : IFacultyService
    {
        private IWebHostEnvironment _environment;
        public IMapper _mapper;

        public FacultyService(IWebHostEnvironment environment, IMapper mapper)
        {
            _environment = environment;
            _mapper = mapper;
        }

        public List<Faculty> getFacultyJson()
        {
            var rootPath = _environment.WebRootPath;
            var fullPath = Path.Combine(rootPath, "document/Faculty.json");
            using StreamReader reader = new(fullPath);
            var json = reader.ReadToEnd();
            List<Faculty> faculties = JsonConvert.DeserializeObject<List<Faculty>>(json);
            return faculties;
        }

        public List<UploadExcel> getUploadExcelJson()
        {
            var rootPath = _environment.WebRootPath;
            var fullPath = Path.Combine(rootPath, "document/UploadExcel.json");
            using StreamReader reader = new(fullPath);
            var json = reader.ReadToEnd();
            List<UploadExcel> UploadExcel = JsonConvert.DeserializeObject<List<UploadExcel>>(json);
            return UploadExcel;
        }

        public async Task<ApiResponseModels<string>> readTextFile(string path)
        {
            var rootPath = _environment.WebRootPath;
            var fullPath = Path.Combine(rootPath, path);
            using StreamReader reader = new(fullPath);
            var json = reader.ReadToEnd();
            
            return new ApiResponseModels<string>
            {
                succeed = true,
                message = json
            };
        }

        public void WriteFacultyJson(List<Faculty> data, string filePath)
        {
            string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);

            File.WriteAllText(filePath, jsonString);

        }

        public void WriteUploadExcelJson(List<UploadExcel> data, string filePath)
        {
            string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);

            File.WriteAllText(filePath, jsonString);

        }

        public async Task<ApiResponseModels<FacultyOutPut>> facultyAdd(FacultyInput value)
        {
            var rootPath = _environment.WebRootPath;
            var fullPath = Path.Combine(rootPath, "document/Faculty.json");
            var getFaculty = getFacultyJson();
            int nextId = getFaculty.Count > 0 ? getFaculty.Max(x => x.Id) + 1 : 1;
            Faculty formValue = new Faculty();
            formValue.Id = nextId;
            formValue.Tittle = value.Tittle;
            formValue.IsActive = true;
            formValue.CreatedAt = DateTime.UtcNow;
            formValue.UpdatedAt = DateTime.UtcNow;
            getFaculty.Add(formValue);
            WriteFacultyJson(getFaculty, fullPath);

            return new ApiResponseModels<FacultyOutPut>
            {
                succeed = true,
                message = "success"
            };
        }


  
        public async Task<ApiResponseModels<bool>> facultyDeleteByID(int id)
        {
            var rootPath = _environment.WebRootPath;
            var fullPath = Path.Combine(rootPath, "document/Faculty.json");

            var facultyData = getFacultyJson();
            Faculty faculty = facultyData.Where(x => x.Id == id).FirstOrDefault();
            facultyData.Remove(faculty);
            WriteFacultyJson(facultyData, fullPath);
            return new ApiResponseModels<bool>
            {
                succeed = true,
                message = "success",
            };
        }
        public async Task<ApiResponseModels<FacultyOutPut>> FacultyEditByID(int id)
        {

            var facultyData = getFacultyJson();
            Faculty faculty = facultyData.Where(x => x.Id == id).FirstOrDefault();
            var rslt = _mapper.Map<FacultyOutPut>(faculty);
            return new ApiResponseModels<FacultyOutPut>
            {
                succeed = true,
                message = "success",
                data = rslt
            };
        }
        public async Task<ApiResponseModels<FacultyOutPut>> facultyUpdate(FacultyInput value)
        {
            var rootPath = _environment.WebRootPath;
            var fullPath = Path.Combine(rootPath, "document/Faculty.json");
            var getFaculty = getFacultyJson();
            Faculty faculty = getFaculty.Where(x => x.Id == value.Id).FirstOrDefault();
            faculty.Tittle = value.Tittle;
            faculty.IsActive = true;
            faculty.UpdatedAt = DateTime.UtcNow;

            // getFaculty.(faculty);
            WriteFacultyJson(getFaculty, fullPath);

            return new ApiResponseModels<FacultyOutPut>
            {
                succeed = true,
                message = "success"
            };
        }

     

    }
}
