using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAdmin.Logic
{
    public class Repository<T> : IRepository<T>
    {
        public Action<Answer> AnswerAdded { get; set; }
        public Action<Question> QuestionAdded { get; set; }

        protected List<T> _items;

        public IEnumerable<T> Data => _items ?? (_items = new List<T>());

        public IEnumerable<T> FindAll(Predicate<T> predicate)
        {
            return _items.FindAll(predicate);
        }

        public virtual void AddItem(T item)
        {
            _items.Add(item);
        }

        public virtual void RemoveItem(T item)
        {
            _items.Remove(item);
        }
    }

    public class QuestionRepository : Repository<Question>
    {

        public QuestionRepository()
        {
            using (var context = new Context())
            {
                _items = context.Questions.ToList();
            }
        }
        
        public override void AddItem(Question question)
        {
            using (var context = new Context())
            {
                context.Set<Question>().Add(question);
                context.SaveChanges();
            }

            _items.Add(question);
            QuestionAdded?.Invoke(question);
            
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

    public class AnswerRepository : Repository<Answer>
    {

        public AnswerRepository()
        {
            using (var context = new Context())
            {
                _items = context.Answers.Include("Question").ToList();
            }
        }
        public override void AddItem(Answer answer)
        {
            using (var context = new Context())
            {
                context.Set<Answer>().Add(answer);
                context.SaveChanges();
            }

            _items.Add(answer);
            AnswerAdded?.Invoke(answer);
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
