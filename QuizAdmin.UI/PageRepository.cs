using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAdmin.UI
{
    public class PageRepository
    {
        private HomePage _homePage;
        private QuestionListPage _questionListPage;

        public HomePage HomePage
        {
            get { return _homePage ?? (_homePage = new HomePage()); }
        }

        public QuestionListPage QuestionListPage
        {
            get { return _questionListPage ?? (_questionListPage = new QuestionListPage()); }
        }
    }
}
