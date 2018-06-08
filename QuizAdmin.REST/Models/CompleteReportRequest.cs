using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizAdmin.REST.Models
{
    public class CompleteReportRequest
    {
        public string Report_Guid { get; set; }
        public string Created { get; set; }
        public string Finished { get; set; }
        public string IsOK { get; set; }
        public List<string> Answers_Id { get; set; }

        public CompleteReportRequest()
        {

        }
    }
}