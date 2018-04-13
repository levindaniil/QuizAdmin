using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAdmin.Logic
{
    public class Answer
    {
        public int Id { get; set; }
        public virtual Question Question { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
