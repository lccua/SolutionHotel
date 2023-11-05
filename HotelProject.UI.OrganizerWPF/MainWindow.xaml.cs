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
using HotelProject.BL.Managers;
using HotelProject.BL.Model;
using HotelProject.Util;
using HotelProject.UI.OrganizerWPF.Model;



namespace HotelProject.UI.OrganizerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ActivityManager activityManager;

        private ObservableCollection<ActivityUI> activitiesUIs = new ObservableCollection<ActivityUI>();

        public MainWindow()
        {
            InitializeComponent();
            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);

            activitiesUIs = new ObservableCollection<ActivityUI>(activityManager.GetActivities().Select(x => new ActivityUI();
            ActivityDataGrid.ItemsSource = activitiesUIs;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ActivityDataGrid.ItemsSource = new ObservableCollection<ActivityUI>(activityManager.GetActivities().Select(x => new ActivityUI();
        }

        private void MenuItemAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            ActivityWindow w = new ActivityWindow(false, null);

            if (w.ShowDialog() == true)
                activitiesUIs.Add(w.activityUI);
        }

        private void MenuItemDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            ActivityUI selectedActivity = (ActivityUI)ActivityDataGrid.SelectedItem;
            int activityId = selectedActivity.Id;
            activityManager.DeleteActivity(activityId);

            activitiesUIs.Remove(selectedActivity);

        }

        private void MenuItemUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null) MessageBox.Show("Customer not selected", "Update");
            else
            {
                ActivityWindow w = new ActivityWindow(true, (ActivityUI)ActivityDataGrid.SelectedItem);
                w.ShowDialog();
            }
        }

        private void OpenAddCustomerWindow(object sender, RoutedEventArgs e)
        {

        }

        private void OpenCustomerOverviewWindow(object sender, RoutedEventArgs e)
        {

        }

        private void OpenDeleteCustomerWindow(object sender, RoutedEventArgs e)
        {

        }

        private void OpenEditCustomerWindow(object sender, RoutedEventArgs e)
        {

        }
    }
}
