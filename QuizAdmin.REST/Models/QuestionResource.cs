using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizAdmin.REST.Models
{
    public class QuestionResource
    {
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Explanation { get; set; }
    }
}