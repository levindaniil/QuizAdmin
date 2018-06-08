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
using QuizAdmin.Logic.Model;
using QuizAdmin.Logic.Repository;

namespace QuizAdmin.UI
{
    /// <summary>
    /// Логика взаимодействия для QuestionListPage.xaml
    /// </summary>
    public partial class QuestionListPage : Page
    {
        
        IRepository<Question> questionsRepo = RepositoryFactory.Default.GetRepository<Question>();
        IRepository<Answer> answerRepo = RepositoryFactory.Default.GetRepository<Answer>();
       

        public Action GoHome;
        public Action<Question> EditWindow;
        public Action OpenStatistics;

        public QuestionListPage()
        {
            InitializeComponent();
            listboxQuestions.ItemsSource = questionsRepo.Data.OrderByDescending(a => a.Date);
            questionsRepo.ItemAdded += a => RefreshListBox();
        }

        private void buttonHome_Click(object sender, RoutedEventArgs e)
        {
            listboxQuestions.SelectedIndex = -1;
            GoHome?.Invoke();
        }


        private void RefreshListBox()
        {
            listboxQuestions.ItemsSource = null;
            listboxQuestions.ItemsSource = questionsRepo.Data.OrderByDescending(a => a.Date);
        }

        private void buttonDeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            var question = listboxQuestions.SelectedItem as Question;
            if (question != null)
            {
                var res = MessageBox.Show("Do you want to delete the question", "Deleting question", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    var answers = questionsRepo.Data.FirstOrDefault(q => q.Id == question.Id).Answers.ToList();                    

                    foreach (var item in answers)
                        answerRepo.RemoveItem(item);

                    questionsRepo.RemoveItem(question);
                    RefreshListBox();
                }               
            }            
            
        }

        private void listboxQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listboxQuestions.SelectedIndex != -1)
            {
                Question selectedQuestion = listboxQuestions.SelectedItem as Question;
                IRepository<Report> reports = RepositoryFactory.Default.GetRepository<Report>() as ReportRepository;
                if (reports.Data.FirstOrDefault(r => r.Question.Id == selectedQuestion.Id) != null)
                {
                    buttonEditQuestion.IsEnabled = false;
                    ToolTip tp = new ToolTip();
                    tp.Content = "You can't edit the question because a report for it is already created"; ;
                    buttonEditQuestion.ToolTip = tp;
                }
                else
                {
                    buttonEditQuestion.ToolTip = null;
                    buttonEditQuestion.IsEnabled = true;
                }
                    
                buttonDeleteQuestion.IsEnabled = true;
                buttonViewStats.IsEnabled = true;
            }

            if(listboxQuestions.SelectedIndex == -1)
            {
                buttonEditQuestion.IsEnabled = false;
                buttonDeleteQuestion.IsEnabled = false;
                buttonViewStats.IsEnabled = false;
            }
        }

        private void buttonEditQuestion_Click(object sender, RoutedEventArgs e)
        {
            var chQ = listboxQuestions.SelectedItem as Question;
            if (chQ == null)
                MessageBox.Show("Choose question", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                EditWindow?.Invoke(chQ);
            }
            RefreshListBox();
        }

        private void buttonViewStats_Click(object sender, RoutedEventArgs e)
        {
            OpenStatistics?.Invoke();
        }
    }
}
