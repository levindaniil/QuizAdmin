using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAdmin.UI
{
    public class PageFactory
    {
        private PageRepository _pageRepository;

        public PageRepository PageRepository
        {
            get
            {
                return _pageRepository ?? (_pageRepository = new PageRepository());
            }
        }

        private static PageFactory _pageFactory;

        public static PageFactory Instance
        {
            get
            {
                return _pageFactory ?? (_pageFactory = new PageFactory());
            }
        }

        private PageFactory() { }
    }
}
