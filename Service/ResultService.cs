using AutoMapper;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MJRPAdmin.Constants;
using MJRPAdmin.DTO.DtoInput;
using MJRPAdmin.DTO.DtoOutPut;
using MJRPAdmin.Entities;
using MJRPAdmin.Misc;
using MJRPAdmin.Models;
using MJRPAdmin.Service.interfaces;
using MJRPAdmin.UnitOfWork;
using Newtonsoft.Json;
using System.Text;

namespace MJRPAdmin.Service
{
    public class ResultService : IResultService
    {
        private IWebHostEnvironment _environment;
        public IMapper _mapper;
        public IFacultyService _facultyService;
        public IDocumentService _documentService;
        public IUnitOfWork _unitOfWork;

        public ResultService(IWebHostEnvironment environment, IMapper mapper, IFacultyService facultyService, IDocumentService documentService, IUnitOfWork unitOfWork)
        {
            _environment = environment;
            _mapper = mapper;
            _facultyService = facultyService;
            _documentService = documentService;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponseModels<UploadExcelOutPut>> uploadResultByExcelFile(UploadExcelInput model)
        {
            string filepath = "";
            string fileName = null;
            string excelDirPath = Path.Combine(_environment.WebRootPath, FilePath.UploadExcelFilePath);
            if (model.ExcleFile != null && model.ExcleFile.Length > 0)
            {
                fileName = MiscMethods.uploadFileToLocal(model.ExcleFile, excelDirPath);
                filepath = Path.Combine(excelDirPath, fileName);
            }

            UploadResult uploadResult = new UploadResult()
            {
                FacultyId = model.FacultyId,
                ResultDescription = Path.GetFileNameWithoutExtension(model.ExcleFile.FileName),
                FileName = fileName,
                ResultDate = DateTime.UtcNow,
                DisplayPriority = 1,
                IsVisible = true,
                ModifyDate = DateTime.UtcNow,
                NoOfRowsToDisplay = model.ResultRow,
                IsNewFormat = true,
            };

            await _unitOfWork.UploadResult.AddAsync(uploadResult);
            await _unitOfWork.SaveAsync();

            int resultId = uploadResult.RecId;
            await uploadResultFromExcel(resultId, filepath, model);
            return new ApiResponseModels<UploadExcelOutPut>
            {
                succeed = true,
                message = "success"
            };
        }


        public async Task<ApiResponseModels<List<UploadResultOutput>>> getUploadExcelFile(int facultyId=1)
        {
            List<UploadResult> uploadResults = _unitOfWork.UploadResult.GetWhere(x => x.FacultyId == facultyId).OrderByDescending(y=>y.ResultDate).ToList();//(await _unitOfWork.UploadResult.GetAllAsync()).ToList();
            //List<UploadResult> uploadResults = _unitOfWork.UploadResult.GetWhere(x => x.FacultyId == facultyId).OrderByDescending(y => y.ResultDate).Take(5000).ToList();//(await _unitOfWork.UploadResult.GetAllAsync()).ToList();
            var rslt = _mapper.Map<List<UploadResultOutput>>(uploadResults);
            return new ApiResponseModels<List<UploadResultOutput>>
            {
                succeed = true,
                message = "success",
                data = rslt
            };
        }


        public async Task<ApiResponseModels<UploadResult>> deleteResult(int recId)
        {
            UploadResult uploadResult = _unitOfWork.UploadResult.GetWhere(x => x.RecId == recId).FirstOrDefault();
            _unitOfWork.UploadResult.Remove(uploadResult);
            _unitOfWork.Save();
            return new ApiResponseModels<UploadResult>
            {
                succeed = true,
                message = "success",
                data = uploadResult
            };
        }

        public async Task<ApiResponseModels<UploadResult>> ShowHide(bool isVisible, int recId)
        {
            UploadResult uploadResult = _unitOfWork.UploadResult.GetWhere(x => x.RecId == recId).FirstOrDefault();
            uploadResult.IsVisible = isVisible;
            _unitOfWork.Save();
            return new ApiResponseModels<UploadResult>
            {
                succeed = true,
                message = "success",
                data = uploadResult
            };
        }


        public async Task<ApiResponseModels<UploadResult>> getResultDetail(int recId)
        {
            UploadResult uploadResult = _unitOfWork.UploadResult.GetWhere(x => x.RecId == recId).FirstOrDefault();
         
            return new ApiResponseModels<UploadResult>
            {
                succeed = true,
                message = "success",
                data = uploadResult
            };
        }

        async Task uploadResultFromExcel(Int64 newResultId, string filePath, UploadExcelInput model)
        {
            string[] colExl = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            int mainHeaderRow = Convert.ToInt32(model.HeaderRow);
            int rowGAP = Convert.ToInt32(model.RowGap);
            int resultRow = Convert.ToInt32(model.ResultRow);
            int readColNumber = 1;

            StringBuilder sbMainHeaderRow = new StringBuilder();
            StringBuilder sbResultRow = new StringBuilder();
            StringBuilder sbResultCol = new StringBuilder();
            StringBuilder sbResult = new StringBuilder();
            StringBuilder sbSingleResultRow = new StringBuilder();
            string rollnumber = string.Empty;
            int rowNumber = 1;
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fs, false))
                    {
                        WorkbookPart workbookPart = doc.WorkbookPart;
                        SharedStringTablePart sstpart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
                        SharedStringTable sst = sstpart.SharedStringTable;

                        WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                        Worksheet sheet = worksheetPart.Worksheet;

                        var cells = sheet.Descendants<Cell>();
                        var rows = sheet.Descendants<Row>();


                        StringBuilder sbH = new StringBuilder();
                        StringBuilder sbData = new StringBuilder();
                        //ResultService rs = new ResultService();
                        sbResult = new StringBuilder();

                        int rowGAPRead = rowGAP;
                        int resultRowRead = resultRow;

                        foreach (Row row in rows)
                        {
                            sbResultCol = new StringBuilder();
                            int itemNumber = 1;

                            string strTD = "";
                            readColNumber = 1;
                            foreach (Cell c in row.ChildElements)
                            {

                                if (c.CellValue != null)
                                {
                                    if (itemNumber != 1)
                                    {
                                        strTD = strTD.Replace("colspan='1'", "colspan='" + itemNumber + "'");
                                        strTD = strTD.Replace("text-align:left", "text-align:center");
                                    }
                                    sbResultCol.Append(strTD.ToString());
                                    strTD = "";
                                    itemNumber = 1;
                                    int ssid = 0;
                                    string str = "";


                                    if (c.DataType == null)
                                    {
                                        str = c.CellValue.Text;
                                    }
                                    else
                                    {
                                        ssid = int.Parse(c.CellValue.Text);
                                        str = sst.ChildElements[ssid].InnerText;
                                    }



                                    if (mainHeaderRow == 0)
                                    {
                                        if (rowGAPRead == 0)
                                        {
                                            if (readColNumber == 3 && rollnumber == "")
                                                rollnumber = str;
                                        }
                                    }

                                    strTD = "<td colspan='1' style=' border: 1px solid black;text-align:left'>" + str + "</td>";

                                }
                                else
                                {
                                    itemNumber++;
                                    // sbResultRow.Append("<td style=' border: 1px solid black;'></td>");
                                }
                                readColNumber++;
                            }
                            if (itemNumber != 1)
                            {
                                strTD = strTD.Replace("colspan='1'", "colspan='" + itemNumber + "'");
                                strTD = strTD.Replace("text-align:left", "text-align:center");
                                sbResultCol.Append(strTD.ToString());
                            }
                            else
                            {
                                sbResultCol.Append(strTD.ToString());
                            }


                            /*mainHeader is the main result header*/
                            if (mainHeaderRow == 0)
                            {
                                /*this is to avoid multiple result header*/

                                if (rowGAPRead != 0)
                                {
                                    rowGAPRead--;
                                }
                                else
                                {
                                    // sbResultRow.Append("<tr>" + sbResultCol.ToString() + "</tr>");
                                    if (resultRowRead != 1)
                                    {
                                        sbSingleResultRow.Append("<tr>" + sbResultCol.ToString() + "</tr>");
                                    }

                                    resultRowRead--;
                                    if (resultRowRead == 0)
                                    {
                                        rowGAPRead = rowGAP;
                                        resultRowRead = resultRow;
                                        sbSingleResultRow.Append("<tr>" + sbResultCol.ToString() + "</tr>");
                                        // sbResultRow.Append(sbSingleResultRow.ToString());

                                        //clsIO.saveTextFile(sbSingleResultRow.ToString(), Server.MapPath(Request.ApplicationPath) + "/upload/NewResults/" + newResultId + rollnumber + ".txt");
                                        sbResult.Append("{\"rollno\":" + "\"" + rollnumber + "\"" + ", \"data\":\"" + sbSingleResultRow.ToString() + "\"},");



                                        rollnumber = "";
                                        sbSingleResultRow = new StringBuilder();
                                    }
                                }


                            }
                            else
                            {
                                sbMainHeaderRow.Append("<tr>" + sbResultCol.ToString() + "</tr>");
                                mainHeaderRow--;
                                if (mainHeaderRow == 0)
                                {
                                    //clsIO.saveTextFile(sbMainHeaderRow.ToString(), Server.MapPath(Request.ApplicationPath) + "/upload/NewResults/" + newResultId + "header" + ".txt");
                                    sbResult.Append("{\"header\"" + ":" + "\"" + sbMainHeaderRow.ToString() + "\",\"result\":[");
                                    rowGAPRead = 0;

                                }
                            }

                            rowNumber++;

                        }
                        //if (value.ExcleFile != null && value.ExcleFile.Length > 0)
                        //{
                        //    string dirpath = Path.Combine(_environment.WebRootPath, "excelDocument", "Excel");
                        //    string filepath = MiscMethods.uploadFileToLocal(value.ExcleFile, dirpath);
                        //    ExcelUrl = filepath;
                        //}

                        string resultDirPath = Path.Combine(_environment.WebRootPath, FilePath.ResultJSONPath);
                        using (var writer = File.CreateText(resultDirPath + "/" + newResultId + ".json"))
                        {
                            await writer.WriteLineAsync(sbResult.ToString() + "{}]}");
                        }

                        //clsIO.saveTextFile(sbResult.ToString() + "{}]}", Server.MapPath(Request.ApplicationPath) + "/upload/NewResults/" + newResultId + ".txt");
                    }
                }
            }
            catch (Exception Ex)
            {
                // Response.Write("<script>alert('File read error. Please contact to developer.');</script>");
                //txtLog.Text = Ex.Message;
            }


        }

    }
}
