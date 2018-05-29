using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizAdmin.REST.Models
{
    public class AnswerResource
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}