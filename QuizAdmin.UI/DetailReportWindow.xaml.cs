using QuizAdmin.Logic.Model;
using QuizAdmin.Logic.Repository;
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
using System.Windows.Shapes;

namespace QuizAdmin.UI
{
    /// <summary>
    /// Логика взаимодействия для DetailReportWindow.xaml
    /// </summary>
    public partial class DetailReportWindow : Window
    {
        IRepository<Report> reportRepo = RepositoryFactory.Default.GetRepository<Report>() as ReportRepository;
        IRepository<Question> questionRepo = RepositoryFactory.Default.GetRepository<Question>() as QuestionRepository;
        Report _report;

        public Action OpenStatistics;

        public DetailReportWindow(Report report)
        {
            InitializeComponent();
            _report = report;

        }

        private void ShowReportDetails()
        {

            var question = questionRepo.Data.FirstOrDefault(q => q.Id == _report.Question.Id);
            var allAnswers = question.Answers;
            var userAnswers = _report.Answers;
            var sp = new StackPanel();

            foreach (var item in allAnswers)
            {
                //if(userAnswers.Contains(item))
                //    {
                //        if (item.IsChecked == true)
                //        {
                //            var tb = item.Content as TextBlock;
                //            tb.Foreground = Brushes.LightGreen;
                //            tb.Text += "  - Correct";
                //            item.Content = tb;
                //            chosenAnswers.Remove(item);
                //        }
                //        else
                //        {
                //            var tb = item.Content as TextBlock;
                //            tb.Foreground = Brushes.OrangeRed;
                //            tb.Text += "  - Correct, Not Chosen";
                //            item.Content = tb;
                //        }
                //    }
                //}

                //    var correctAnswer = answers.FirstOrDefault(a => a.IsCorrect == true);
                //    Answer chosenAnswer;
                //    var chosenCB = radioButtons.FindAll(a => a.IsChecked == true);
                //    //мы до этого определяем вид элементов grid, поэтому эта проверка не нужна

                //    //if (chosenCB.Count() > 1)
                //    //{
                //    //    throw new Exception("Something went wrong");

                //    //}
                //    //else
                //    chosenAnswer = answers.FirstOrDefault(a => a.Id == int.Parse(chosenCB[0].Name.Substring(11)));


                //    gridAnswers.Children.Clear();
                //    gridAnswers.RowDefinitions.Clear();

                //    StackPanel sp = new StackPanel();

                //    TextBlock tbChose = new TextBlock
                //    {
                //        FontSize = 27
                //    };
                //    if (chosenAnswer.Id == correctAnswer.Id)
                //        tbChose.Foreground = Brushes.LightGreen;
                //    else
                //        tbChose.Foreground = Brushes.OrangeRed;
                //    tbChose.Text = "Your choice: ";

                //    TextBlock tbChoseAns = new TextBlock
                //    {
                //        FontSize = 27,
                //        Foreground = Brushes.White,
                //        TextWrapping = TextWrapping.Wrap,
                //        Text = $"{chosenAnswer.Text}"
                //    };

                //    StackPanel spChose = new StackPanel
                //    {
                //        Orientation = Orientation.Horizontal
                //    };
                //    spChose.Children.Add(tbChose);
                //    spChose.Children.Add(tbChoseAns);


                //    TextBlock tbCorr = new TextBlock
                //    {
                //        FontSize = 27,
                //        Text = "Correct answer: ",
                //        Foreground = Brushes.LightGreen
                //    };

                //    TextBlock tbCorrAns = new TextBlock
                //    {
                //        FontSize = 27,
                //        Foreground = Brushes.White,
                //        TextWrapping = TextWrapping.Wrap,
                //        Text = $"{correctAnswer.Text}"
                //    };

                //    StackPanel spCorr = new StackPanel
                //    {
                //        Orientation = Orientation.Horizontal
                //    };
                //    spCorr.Children.Add(tbCorr);
                //    spCorr.Children.Add(tbCorrAns);



                //    TextBlock expl = new TextBlock
                //    {
                //        FontSize = 27,
                //        Foreground = Brushes.White,
                //        TextWrapping = TextWrapping.Wrap,
                //        Text = $"Explanation: {question.Explanation}"
                //    };

                //    sp.Children.Add(spChose);
                //    sp.Children.Add(spCorr);
                //    sp.Children.Add(expl);

                //    gridAnswers.Children.Add(sp);
                //}

                //buttonSubmit.Visibility = Visibility.Hidden;
            }
        }
    }
}
