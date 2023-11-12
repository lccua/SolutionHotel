using HotelProject.BL.Managers;
using HotelProject.BL.Model;
using HotelProject.UI.OrganizerWPF.Model;
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

namespace HotelProject.UI.OrganizerWPF
{
    /// <summary>
    /// Interaction logic for ActivityWindow.xaml
    /// </summary>
    public partial class ActivityWindow : Window
    {
        public ActivityUI activityUI;

        private ActivityManager activityManager;
        public ActivityWindow(ActivityUI activityUI)
        {
            InitializeComponent();
            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);

            this.activityUI = activityUI;

           
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate and parse input values
                if (string.IsNullOrWhiteSpace(AdultPriceTextBox.Text) ||
                    string.IsNullOrWhiteSpace(ChildPriceTextBox.Text) ||
                    string.IsNullOrWhiteSpace(DiscountTextBox.Text) ||
                    string.IsNullOrWhiteSpace(AvailableSpotsTextBox.Text) ||
                    string.IsNullOrWhiteSpace(DurationTextBox.Text) ||
                    string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                    SheduledDate.SelectedDate == null)
                {
                    throw new ArgumentException("Input fields cannot be empty");
                }

                // Replace '.' with ',' in price TextBoxes
                string adultPriceText = AdultPriceTextBox.Text.Replace('.', ',');
                string childPriceText = ChildPriceTextBox.Text.Replace('.', ',');

                decimal adultPriceParsed = decimal.Parse(adultPriceText);
                decimal childPriceParsed = decimal.Parse(childPriceText);
                int discountParsed = int.Parse(DiscountTextBox.Text);
                int availableSpotsParsed = int.Parse(AvailableSpotsTextBox.Text);
                int durationParsed = int.Parse(DurationTextBox.Text);

                DateTime? scheduledDate = SheduledDate.SelectedDate;
                string scheduledDateParsed = scheduledDate.Value.ToString("yyyy-MM-dd");

                // Create Activity object
                Activity a = new Activity(NameTextBox.Text, new ActivityInfo(DescriptionTextBox.Text, new Address(CityTextBox.Text, ZipCodeTextBox.Text, HouseNumberTextBox.Text, StreetTextBox.Text), durationParsed), scheduledDateParsed, availableSpotsParsed, adultPriceParsed, childPriceParsed, discountParsed);

                // Add the activity
                int currentId = activityManager.AddActivity(a);
                a.Id = currentId;

                // Create ActivityUI object
                activityUI = new ActivityUI(a.Id, a.Name, a.ScheduledDate, a.AvailableSpots, a.AdultPrice, a.ChildPrice, a.Discount, a.ActivityInfo.Description, a.ActivityInfo.Duration, a.ActivityInfo.Address.ToString());

                DialogResult = true;
                Close();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Invalid input format. Please enter valid numeric values.");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

