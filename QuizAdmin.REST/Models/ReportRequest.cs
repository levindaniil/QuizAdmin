using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizAdmin.REST.Models
{
    public class ReportRequest
    {
        [JsonProperty("user")]
        public string User { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }

        public ReportRequest()
        {
            
        }
    }
}