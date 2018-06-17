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
    /// Логика взаимодействия для UserReportPage.xaml
    /// </summary>
    /// 


    public partial class UserReportPage : Page
    {
        IRepository<User> reportsUser = RepositoryFactory.Default.GetRepository<User>() as UserRepository;
        IRepository<Report> reportsRepo = RepositoryFactory.Default.GetRepository<Report>() as ReportRepository;
        public Action GoHome;
        public Action<User> ShowMore;
        Dictionary<User, int> _userDict;

        public UserReportPage()
        {
            InitializeComponent();
            reportsUser.ItemAdded += a => RefreshListBox();
            _userDict = CreateUserDictionary();
            listboxUserReports.ItemsSource = _userDict;
        }

        private Dictionary<User,int> CreateUserDictionary()
        {
            var userDict = new Dictionary<User, int>();
            foreach(var user in reportsUser.Data)
            {
                int score = 0;
                var userReport = reportsRepo.Data.Where(r => r.User.Id == user.Id);
                foreach(var report in userReport)
                {
                    if (report.IsOK == true) score++;
                    else if (report.IsOK == false) score--;
                }
                userDict[user] = score;
            }
            return userDict;
        }

        private void RefreshListBox()
        {
            listboxUserReports.ItemsSource = null;
            listboxUserReports.ItemsSource = reportsUser.Data;
        }

        private void buttonHome_Click(object sender, RoutedEventArgs e)
        {
            listboxUserReports.SelectedIndex = -1;
            GoHome?.Invoke();
        }

        private void buttonOrder_Click(object sender, RoutedEventArgs e)
        {
            if (comboboxOrderType.SelectedItem != null)
            {
                if (comboboxOrderType.SelectedItem.ToString() == "Ascending")
                {
                    listboxUserReports.ItemsSource = reportsUser.Data.OrderBy(r => r.Key);
                }

                else
                    listboxUserReports.ItemsSource = reportsUser.Data.OrderByDescending(r => r.Key);
            }
            else
                MessageBox.Show("Please, choose order parameter!");
        }

        private void buttonMoreInfo_Click(object sender, RoutedEventArgs e)
        {
            if (listboxUserReports.SelectedItem != null)
            {
                var selectedUser = listboxUserReports.SelectedItem as User;
                ShowMore?.Invoke(selectedUser);
                listboxUserReports.SelectedIndex = -1;
            }
            else
                MessageBox.Show("Please, select a user");
        }
    }
}
