using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAdmin.Logic.Model
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Explanation { get; set; }
        public virtual List<Answer> Answers { get; set; }

        public string ShortDate => Date.ToShortDateString();
    }
}
