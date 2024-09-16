using System;
using System.Collections.Generic;

namespace MJRPAdmin.Entities
{
    public partial class AcademicProg
    {
        public int AcdProId { get; set; }
        public string? AcdProNm { get; set; }
        public string? HeaderImg { get; set; }
        public string? AcdProDesc { get; set; }
        public ulong? AcdProIsDeleted { get; set; }
        public int? DisplayPriority { get; set; }
    }
}
