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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuizAdmin.UI
{
    /// <summary>
    /// Логика взаимодействия для ReportListPage.xaml
    /// </summary>
    public partial class ReportListPage : Page
    {
        IRepository<Report> reportsRepo = RepositoryFactory.Default.GetRepository<Report>() as ReportRepository;
        public Action GoHome;
        public Action GoBack;
        User _user;

        public ReportListPage(User user)
        {
            InitializeComponent();
            _user = user;
            listboxReports.ItemsSource = reportsRepo.Data.Where(r => r.User.Id == user.Id);
            reportsRepo.ItemAdded += a => RefreshListBox();
        }

        private void RefreshListBox()
        {
            listboxReports.ItemsSource = null;
            listboxReports.ItemsSource = reportsRepo.Data.OrderByDescending(a => a.Replied);
        }

        private void buttonHome_Click(object sender, RoutedEventArgs e)
        {
            listboxReports.SelectedIndex = -1;
            GoHome?.Invoke();
        }


        // Sorting, logic template.
        private void buttonOrder_Click(object sender, RoutedEventArgs e)
        {
            if (comboboxOrderType.SelectedItem != null)
            {
                var type = comboboxOrderType.SelectionBoxItem.ToString();
                if (type == "Ascending")
                {
                    listboxReports.ItemsSource = reportsRepo.Data.Where(u => u.User.Key == _user.Key).OrderBy(r => r.Question.Date);
                }
                else if (type == "Descending")
                {
                    listboxReports.ItemsSource = reportsRepo.Data.Where(u => u.User.Key == _user.Key).OrderByDescending(r => r.Question.Date);
                }
            }
            else
                MessageBox.Show("Please, choose order parameter!");
        }

        private void buttonGoBack_Click(object sender, RoutedEventArgs e)
        {
            listboxReports.SelectedIndex = -1;
            GoBack?.Invoke();
        }
    }

    //private void lbi_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    //{

    //}
}

