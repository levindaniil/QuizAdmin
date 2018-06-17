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
        public Action GoHome;
        public Action<User> ShowMore;

        public UserReportPage()
        {
            InitializeComponent();
            listboxUserReports.ItemsSource = reportsUser.Data;//.OrderByDescending(a => a.Replied);
            reportsUser.ItemAdded += a => RefreshListBox();
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
