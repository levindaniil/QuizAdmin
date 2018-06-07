using QuizAdmin.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAdmin.Logic.Repository
{
    public class QuestionRepository :Repository<Question>
    {
        public QuestionRepository()
        {
            using (var context = new Context())
            {
                _items = context.Questions.Include("Answers").ToList();
            }
        }

        public override Question AddItem(Question question)
        {
            using (var context = new Context())
            {
                context.Set<Question>().Add(question);
                context.SaveChanges();
            }

            _items.Add(question);
            ItemAdded?.Invoke(question);
            return question;
        }

        public override Question EditItem(Question question, object id)
        {
            using (var context = new Context())
            {
                var questionToEdit = context.Questions.Include("Answers").FirstOrDefault(q => q.Id == (int)id);
                //context.Set<Question>().Remove(questionToEdit);
                //context.SaveChanges();
                //context.Set<Question>().Add(question);
                questionToEdit.Date = question.Date;
                questionToEdit.Explanation = question.Explanation;
                questionToEdit.Text = question.Text;
                questionToEdit.Answers = question.Answers;
                ItemAdded?.Invoke(question);
                context.SaveChanges();
                return question;
            }
        }

        public override void RemoveItem(Question question)
        {
            using (var context = new Context())
            {
                var questionToRemove = context.Questions.Include("Answers").FirstOrDefault(q => q.Id == question.Id);
                context.Answers.RemoveRange(questionToRemove.Answers);
                context.Set<Question>().Remove(questionToRemove);
                context.SaveChanges();
            }

            _items.Remove(question);
        }
    }
}
