using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizAdmin.Logic.Model;

namespace QuizAdmin.Logic.Repository
{
    public class RepositoryFactory
    {
        private RepositoryFactory() { }

        private static RepositoryFactory _default;

        public static RepositoryFactory Default
        {
            get
            {
                if (_default == null)
                    _default = new RepositoryFactory();
                return _default;
            }
        }

        private IRepository<Answer> AnswerRepo;
        private IRepository<Question> QuestionRepo;
        private IRepository<User> UserRepo;
        private IRepository<Report> ReportRepo;

        public IRepository<T> GetRepository<T>()
        {
            if (typeof(T) == typeof(Answer))
                return (IRepository<T>)AnswerRepo ?? ((AnswerRepo = new AnswerRepository()) as IRepository<T>);
            else if (typeof(T) == typeof(Question))
                return (IRepository<T>)QuestionRepo ?? ((QuestionRepo = new QuestionRepository()) as IRepository<T>);
            else if (typeof(T) == typeof(User))
                return (IRepository<T>)UserRepo ?? ((UserRepo = new UserRepository()) as IRepository<T>);
            else if (typeof(T) == typeof(Question))
                return (IRepository<T>)ReportRepo ?? ((ReportRepo = new ReportRepository()) as IRepository<T>);
            else
                throw new Exception("No repository");
        }





    }
}
