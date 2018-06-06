using QuizAdmin.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAdmin.Logic.Repository
{
    public class AnswerRepository : Repository<Answer>
    {
        public AnswerRepository()
        {
            using (var context = new Context())
            {
                _items = context.Answers.ToList();
            }
        }

        public override Answer AddItem(Answer answer)
        {
            using (var context = new Context())
            {
                context.Set<Answer>().Add(answer);
                context.SaveChanges();
            }

            _items.Add(answer);
            ItemAdded?.Invoke(answer);

            return answer;
        }

        public override Answer EditItem(Answer item, object id)
        {
            using (var context = new Context())
            {
                if (!(String.IsNullOrEmpty(item.Text)))
                {
                    var answer = context.Answers.FirstOrDefault(a => a.Id == item.Id);
                    answer.IsCorrect = item.IsCorrect;
                    answer.Text = item.Text;
                    context.SaveChanges();
                    return answer;
                }
            }
            return null;
        }

        public override void RemoveItem(Answer answer)
        {
            using (var context = new Context())
            {
                context.Set<Answer>().Remove(context.Answers.FirstOrDefault(a => a.Id == answer.Id));
                context.SaveChanges();
            }

            _items.Remove(answer);
        }

    }
}
