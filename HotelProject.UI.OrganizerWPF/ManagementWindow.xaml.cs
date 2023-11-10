using HotelProject.BL.Managers;
using HotelProject.BL.Model;
using HotelProject.DL.Repositories;
using HotelProject.UI.OrganizerWPF.Model;
using HotelProject.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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



namespace HotelProject.UI.OrganizerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ManagementWindow : Window
    {
        private ActivityManager activityManager;

        private ObservableCollection<ActivityUI> activitiesUIs = new ObservableCollection<ActivityUI>();

        public ManagementWindow()
        {
            InitializeComponent();
            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);

            activitiesUIs = new ObservableCollection<ActivityUI>(activityManager.GetActivities(null).Select(x => new ActivityUI(x.Id, x.Name, x.ScheduledDate, x.AvailableSpots, x.AdultPrice, x.ChildPrice, x.Discount, x.ActivityInfo.Desciption, x.ActivityInfo.Duration, x.ActivityInfo.Address.ToString())));
            ActivityDataGrid.ItemsSource = activitiesUIs;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ActivityDataGrid.ItemsSource = new ObservableCollection<ActivityUI>(activityManager.GetActivities(SearchTextBox.Text).Select(x => new ActivityUI(x.Id, x.Name, x.ScheduledDate, x.AvailableSpots, x.AdultPrice, x.ChildPrice, x.Discount, x.ActivityInfo.Desciption, x.ActivityInfo.Duration, x.ActivityInfo.Address.ToString())));
        }

        private void MenuItemAddActivity_Click(object sender, RoutedEventArgs e)
        {
            ActivityWindow w = new ActivityWindow(null);

            if (w.ShowDialog() == true)
                activitiesUIs.Add(w.activityUI);
        }

    }
}
