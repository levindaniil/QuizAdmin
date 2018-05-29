using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizAdmin.REST.Models
{
    public class ReportResource
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public QuestionResource Question { get; set; }
        public List<AnswerResource> Answers { get; set; }
    }
}