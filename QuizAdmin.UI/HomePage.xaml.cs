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

namespace QuizAdmin.UI
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        public Action OpenQuestions;
        public Action AddQuestion;
        public Action OpenStatistics;

        private void buttonViewQuestions_Click(object sender, RoutedEventArgs e)
        {
            OpenQuestions?.Invoke();
        }

        private void buttonAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            AddQuestion?.Invoke();
        }

        private void buttonViewStats_Click(object sender, RoutedEventArgs e)
        {
            OpenStatistics?.Invoke();
        }
    }
}
