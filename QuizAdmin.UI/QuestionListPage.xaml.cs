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
    /// Логика взаимодействия для QuestionListPage.xaml
    /// </summary>
    public partial class QuestionListPage : Page
    {
        IRepository<Question> questionsRepo = Factory.Default.GetRepository<Question>();
        List<Question> questions;

        public QuestionListPage()
        {
            InitializeComponent();            
            listboxQuestions.ItemsSource = questionsRepo.Data.OrderByDescending(a => a.Date);


        }
    }
}
