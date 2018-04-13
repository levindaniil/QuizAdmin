using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAdmin.Logic
{
    public class Factory
    {
        private Factory() { }

        private static Factory _default;

        public static Factory Default
        {
            get
            {
                if (_default == null)
                    _default = new Factory();
                return _default;
            }
        }

        private IRepository<Answer> AnswerRepo;
        private IRepository<Question> QuestionRepo;

        public IRepository<T> GetRepository<T>()
        {
            if (typeof(T) == typeof(Answer))
                return (IRepository<T>)AnswerRepo ?? ((AnswerRepo = new AnswerRepository()) as IRepository<T>);
            else if (typeof(T) == typeof(Question))
                return (IRepository<T>)QuestionRepo ?? ((QuestionRepo = new QuestionRepository()) as IRepository<T>);
            else
                throw new Exception("No repository");
        }





    }
}
