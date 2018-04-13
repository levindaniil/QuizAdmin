using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuizAdmin.Logic;


namespace QuizAdmin.UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IRepository<Question> questionRepository = Factory.Default.GetRepository<Question>();
    

        public MainWindow()
        {
            InitializeComponent();
            PageFactory.Instance.PageRepository.HomePage.OpenQuestions += OpenQuestions;
            PageFactory.Instance.PageRepository.HomePage.AddQuestion += CreateNewQuestion;
            PageFactory.Instance.PageRepository.QuestionListPage.GoHome += GoHome;
            mainFrame.NavigationService.Navigate(PageFactory.Instance.PageRepository.HomePage);
        }

        private void OpenQuestions()
        {
            mainFrame.NavigationService.Navigate(PageFactory.Instance.PageRepository.QuestionListPage);
        }

        private void CreateNewQuestion()
        {
            mainFrame.NavigationService.Navigate(new AddQuestionPage());
        }

        private void GoHome()
        {
            mainFrame.NavigationService.Navigate(PageFactory.Instance.PageRepository.HomePage);
        }

        
    }
}
//var question = new Question()
//{
//    Date = DateTime.Now.Date,
//    Text = "Are you a robot?",
//    Explanation = "No, I am an idiot"
//};
//questionRepository.AddItem(question);
//int id = question.Id;