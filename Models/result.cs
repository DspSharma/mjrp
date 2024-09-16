namespace MJRPAdmin.Models
{
    public class result
    {
        public int SNo { get; set; }
        public string SubjectCode { get; set; }
        public string Roll { get; set; }
        public string RoolNo { get; set; }
        public string SubjectName { get; set; }
        public string MaxFinal { get; set; }
        public string MaxTermSession { get; set; }
        public string MaxMain { get; set; }

        public string MinFinal { get; set; }
        public string MinTermSession { get; set;}
        public string MinMani { get; set; }

        public string ObtFinal { get; set; }
        public string ObtTermSession { get;set;}
        public string ObtMani { get; set; }
        public string Rmk { get; set; }
    }



    public class ResultData
    {
        public string header { get; set; }
        public List<ResultCollection> result { get; set; }
    }

    public class ResultCollection
    {
        public string rollno { get; set; }
        public string data { get; set; }
    }


}
