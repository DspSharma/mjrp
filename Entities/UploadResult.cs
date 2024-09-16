using System;
using System.Collections.Generic;

namespace MJRPAdmin.Entities
{
    public partial class UploadResult
    {
        public int RecId { get; set; }
        public int? CollegeId { get; set; }
        public int? FacultyId { get; set; }
        public string? ResultDescription { get; set; }
        public string? FileName { get; set; }
        public DateTime? ResultDate { get; set; }
        public int? DisplayPriority { get; set; }
        public bool? IsVisible { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? NoOfRowsToDisplay { get; set; }
        public bool? IsNewFormat { get; set; }
    }
}
