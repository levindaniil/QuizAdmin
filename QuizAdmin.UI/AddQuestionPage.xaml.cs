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
    /// Логика взаимодействия для AddQuestionPage.xaml
    /// </summary>
    public partial class AddQuestionPage : Page
    {
        IRepository<Question> questionsRepo = RepositoryFactory.Default.GetRepository<Question>() as QuestionRepository;
        IRepository<Answer> answerRepo = RepositoryFactory.Default.GetRepository<Answer>() as AnswerRepository;
        Dictionary<string, int> chbAnswerDict;
        
        Question question;

        public AddQuestionPage(Question _question)
        {
            InitializeComponent();
            question = _question;
            chbAnswerDict = new Dictionary<string, int>(); 
        }

        private void buttonAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (question.Id == 0)
            {
                var newQuestion = new Question
                {
                    Date = (DateTime)datePicker.SelectedDate,
                    Text = textBoxQuestionText.Text,
                    Explanation = textBoxExplanation.Text,
                };

                List<Answer> answers = new List<Answer>
            {
                new Answer
                {
                    Text = textBox1.Text,
                    IsCorrect = (bool)checkBox1.IsChecked
                },
                new Answer
                {
                    Text = textBox2.Text,
                    IsCorrect = (bool)checkBox2.IsChecked
                },
                new Answer
                {
                    Text = textBox3.Text,
                    IsCorrect = (bool)checkBox3.IsChecked
                },
                new Answer
                {
                    Text = textBox4.Text,
                    IsCorrect = (bool)checkBox4.IsChecked
                }
            };
                foreach (var item in answers)
                {
                    if (!String.IsNullOrEmpty(item.Text))
                        answerRepo.AddItem(item);
                }

                newQuestion.Answers = answers;
                questionsRepo.AddItem(newQuestion);
                
                MessageBox.Show("Your question was successefully added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                GoHome?.Invoke();
            }

            //редактирование вопроса и ответов к нему

            else
            {
                question.Date = (DateTime)datePicker.SelectedDate;
                question.Explanation = textBoxExplanation.Text;
                question.Text = textBoxQuestionText.Text;
                questionsRepo.EditItem(question, question.Id);
                bool res = true;
                var removeAnswers = new List<Answer>();
                var checkBoxes = GetCheckBoxes();
                foreach (var answer in question.Answers)
                {
                    CheckBox cb = checkBoxes.FirstOrDefault(c => chbAnswerDict[c.Name] == answer.Id);
                    var tb = cb.Content as TextBox;
                    if (!String.IsNullOrEmpty(tb.Text))
                    {
                        answer.Text = tb.Text;
                        answer.IsCorrect = (bool)cb.IsChecked;
                        answerRepo.EditItem(answer, answer.Id);
                    }

                    else if (String.IsNullOrEmpty(tb.Text) && (bool)cb.IsChecked)
                    {
                        MessageBox.Show("You can't pick empty answer as a correct one", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        res = false;
                    }

                    else
                    {
                        answerRepo.RemoveItem(answer);
                        removeAnswers.Add(answer);
                    }
                    checkBoxes.Remove(cb);
                }

                if (removeAnswers.Count > 0)
                    foreach (var answer in removeAnswers)
                        question.Answers.Remove(answer);

                if (checkBoxes.Count > 0)
                {
                    foreach (var cb in checkBoxes)
                    {
                        var tb = cb.Content as TextBox;
                        if (!String.IsNullOrEmpty(tb.Text))
                        {
                            Answer newAnswer = new Answer
                            {
                                Text = tb.Text,
                                IsCorrect = (bool)cb.IsChecked
                            };
                            answerRepo.AddItem(newAnswer);
                            question.Answers.Add(newAnswer);
                        }
                        else if (String.IsNullOrEmpty(tb.Text)&& (bool)cb.IsChecked)
                        {
                            MessageBox.Show("You can't pick empty answer as a correct one", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            res = false;
                        }
                            
                    }
                }

                if (res)
                {
                    MessageBox.Show("Your question was successefully edited", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    GoHome?.Invoke();
                }
            }
            
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
                if (question != null)
                {
                    var answers = question.Answers;
                    List<CheckBox> checkBoxes = GetCheckBoxes();

                    FillChBs(checkBoxes, answers);

                    textBoxExplanation.Text = question.Explanation;
                    datePicker.SelectedDate = question.Date;
                }
            }
        }

        private void FillChBs(List<CheckBox> chbs, List<Answer> answers)
        {
            var c = answers.Count();
            if (c > 4)
            {
                MessageBox.Show("Too many answers, can't display all", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                c = 4;
            }

            for (int i = 0; i < c; i++)
            {
                var cb = chbs.FirstOrDefault(a => a.Name.Contains((i + 1).ToString()));
                var tb = cb.Content as TextBox;
                tb.Text = answers[i].Text;
                tb.ToolTip = new ToolTip { Content = "Leave an empty space to delete an answer" };
                cb.Content = tb;
                cb.IsChecked = answers[i].IsCorrect;
                chbAnswerDict.Add(cb.Name, answers[i].Id);
            }
        }

        private List<CheckBox> GetCheckBoxes()
        {
            var checkBoxes = new List<CheckBox>();
            foreach (var item in stackPanelNewQuestion.Children)
                if (item is CheckBox)
                    checkBoxes.Add(item as CheckBox);

            return checkBoxes;
        }
    }
}
