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
        Question question;

        public AddQuestionPage(Question _question)
        {
            InitializeComponent();
            question = _question;
        }

        private void buttonAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            if(question.Id == 0)
            {
                var newQuestion = new Question
                {

                    Date = (DateTime)datePicker.SelectedDate,
                    Text = textBoxQuestionText.Text,
                    Explanation = textBoxExplanation.Text
                };
                questionsRepo.AddItem(newQuestion);
                int id = newQuestion.Id;
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
                

                MessageBox.Show("Your question was successefully added");
                
            }

            else
            {
                questionsRepo.EditItem(question, (DateTime)datePicker.SelectedDate, textBoxExplanation.Text, textBoxQuestionText.Text);

            }

            GoHome?.Invoke();
        }




        public Action GoHome;

        private void buttonHome_Click(object sender, RoutedEventArgs e)
        {
            GoHome?.Invoke();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (question.Id != 0)
            {
                textBoxQuestionText.Text = question.Text;
                var answers = answerRepo.Data.ToList().FindAll(a => a.Question_Id == question.Id).ToList();
                List<CheckBox> checkBoxes = new List<CheckBox>();

                foreach (var item in stackPanelNewQuestion.Children)
                    if (item is CheckBox)
                        checkBoxes.Add(item as CheckBox);

                FillChBs(checkBoxes, answers);

                textBoxExplanation.Text = question.Explanation;
                datePicker.SelectedDate = question.Date;
            }
        }

        private void FillChBs(List<CheckBox> chbs, List<Answer> answers)
        {
            var c = answers.Count();
            if (c > 4)
            {
                MessageBox.Show("Too many answers, can't display all");
                c = 4;
            }

            for (int i = 0; i < c; i++)
            {
                var cb = chbs.FirstOrDefault(a => a.Name.Contains((i + 1).ToString()));
                var tb = cb.Content as TextBox;
                tb.Text = answers[i].Text;
                cb.Content = tb;
                cb.IsChecked = answers[i].IsCorrect;
            }
        }
    }
}
