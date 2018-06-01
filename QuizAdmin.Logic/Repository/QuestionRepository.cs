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
                _items = context.Questions.ToList();
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
                var questionToEdit = context.Questions.FirstOrDefault(q => q.Id == question.Id);
                questionToEdit.Date = question.Date;
                questionToEdit.Explanation = question.Explanation;
                questionToEdit.Text = question.Text;

                ItemAdded?.Invoke(question);
                context.SaveChanges();
            }
            return null;
        }

        public override void RemoveItem(Question question)
        {
            using (var context = new Context())
            {
                context.Set<Question>().Remove(context.Questions.FirstOrDefault(q => q.Id == question.Id));
                context.SaveChanges();
            }

            _items.Remove(question);
        }
    }
}
