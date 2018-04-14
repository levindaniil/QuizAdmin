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
    /// Логика взаимодействия для AddQuestionPage.xaml
    /// </summary>
    public partial class AddQuestionPage : Page
    {
        IRepository<Question> questionsRepo = Factory.Default.GetRepository<Question>();
        IRepository<Answer> answerRepo = Factory.Default.GetRepository<Answer>();

        public AddQuestionPage()
        {
            InitializeComponent();
            
        }

        private void buttonAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            var question = new Question
            {

                Date = (DateTime)datePicker.SelectedDate,
                Text = textBoxQuestionText.Text,
                Explanation = textBoxExplanation.Text
            };
            questionsRepo.AddItem(question);
            int id = question.Id;
            List<Answer> answers = new List<Answer>
            {
                new Answer
                {
                    Question_Id = id,
                    Text = textBox1.Text,
                    IsCorrect = (bool)checkBox1.IsChecked
                },
                new Answer
                {
                    Question_Id = id,
                    Text = textBox2.Text,
                    IsCorrect = (bool)checkBox2.IsChecked
                },
                new Answer
                {
                    Question_Id = id,
                    Text = textBox3.Text,
                    IsCorrect = (bool)checkBox3.IsChecked
                },
                new Answer
                {
                    Question_Id = id,
                    Text = textBox4.Text,
                    IsCorrect = (bool)checkBox4.IsChecked
                }
            };

            foreach (var item in answers)
            {
                if (!String.IsNullOrEmpty(item.Text))
                    answerRepo.AddItem(item);
            }

            MessageBox.Show("You question was successefully added");
            GoHome?.Invoke();
        }




        public Action GoHome;

        private void buttonHome_Click(object sender, RoutedEventArgs e)
        {
            GoHome?.Invoke();
        }
    }
}
