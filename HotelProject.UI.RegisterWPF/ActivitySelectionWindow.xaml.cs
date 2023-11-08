using HotelProject.BL.Managers;
using HotelProject.BL.Model;
using HotelProject.Util;
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

namespace HotelProject.UI.RegisterWPF
{
    /// <summary>
    /// Interaction logic for ActivitySelectionWindow.xaml
    /// </summary>
    public partial class ActivitySelectionWindow : Window
    {
        private ActivityManager activityManager;
        public List<Activity> activities;
        public string selectedActivityName = "";


        public ActivitySelectionWindow()
        {
            InitializeComponent();

            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
            activities = activityManager.GetActivities(null);


            // Bind the ComboBox to the list of customer names

            foreach (Activity activity in activities)
            {
                ActivityComboBox.Items.Add(activity);
            }

         

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void ActivityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Activity selectedActivity = (Activity)ActivityComboBox.SelectedItem;

            selectedActivityName = selectedActivity.Name;

            ActivityNameTextBox.Text = selectedActivityName;
            SheduledDateTextBox.Text = selectedActivity.ScheduledDate;
            AvailableSpotsTextBox.Text = selectedActivity.AvailableSpots.ToString();
            AdultPriceTextBox.Text = selectedActivity.AdultPrice.ToString();
            ChildPriceTextBox.Text = selectedActivity.ChildPrice.ToString();
            DiscountTextBox.Text = selectedActivity.Discount.ToString();
            DescriptionTextBox.Text = selectedActivity.ActivityInfo.Desciption;
            DurationTextBox.Text = selectedActivity.ActivityInfo.Duration.ToString();
            ZipCodeTextBox.Text = selectedActivity.ActivityInfo.Address.ZipCode;
            CityTextBox.Text = selectedActivity.ActivityInfo.Address.Municipality;
            StreetTextBox.Text = selectedActivity.ActivityInfo.Address.Street;
            HouseNumberTextBox.Text = selectedActivity.ActivityInfo.Address.HouseNumber;
        }
    }
}
