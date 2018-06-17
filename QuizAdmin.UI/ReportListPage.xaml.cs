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

        public ReportListPage()
        {
            InitializeComponent();
            
            listboxReports.ItemsSource = reportsRepo.Data.OrderByDescending(a => a.Replied);
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
                if (comboboxOrderType.SelectedItem.ToString() == "Ascending")
                {
                    listboxReports.ItemsSource = reportsRepo.Data.OrderBy(r =>r.Replied);
                }
                else
                    listboxReports.ItemsSource = reportsRepo.Data.OrderByDescending(r => r.Replied);
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

