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
        IRepository<Answer> answerRepo = Factory.Default.GetRepository<Answer>();
       

        public Action GoHome;
        public Action<Question> EditWindow;

        public QuestionListPage()
        {
            InitializeComponent();
            listboxQuestions.ItemsSource = questionsRepo.Data.OrderByDescending(a => a.Date);
            questionsRepo.QuestionAdded += a => RefreshListBox();
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
                var answers = answerRepo.FindAll(a => a.Question_Id == question.Id);

                foreach (var item in answers)
                    answerRepo.RemoveItem(item);

                questionsRepo.RemoveItem(question);
            }            

            RefreshListBox();
        }

        private void listboxQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listboxQuestions.SelectedIndex != -1)
            {
                buttonEditQuestion.IsEnabled = true;
                buttonDeleteQuestion.IsEnabled = true;
                buttonViewStats.IsEnabled = true;
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
        }
    }
}
