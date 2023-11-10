using HotelProject.BL.Managers;
using HotelProject.Util;
using HotelProject.BL.Model;
using HotelProject.UI.CustomerWPF.Model;
using HotelProject.UI.CustomerWPF;
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
using System.Runtime.ConstrainedExecution;
using HotelProject.BL.Model;

namespace HotelProject.UI.RegisterWPF
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private Customer selectedCustomer; // Member variable to store the selected customer
        private List<Member> selectedMembers;
        private Activity selectedActivity;
        private RegistrationManager registrationManager;
        decimal totalPrice = 0;

        public RegistrationWindow(Customer customer)
        {
            InitializeComponent();
            registrationManager = new RegistrationManager(RepositoryFactory.RegistrationRepository);

            selectedCustomer = customer; // Store the selected customer



            CustomerNameTextBox.Text = selectedCustomer.Name;


            List<Member> members = selectedCustomer.GetMembers();

            // Clear existing items before setting the ItemsSource
            MembersListBox.Items.Clear();
            MembersListBox.ItemsSource = members;

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Registration newRegistration = new Registration(selectedCustomer, selectedActivity, selectedMembers, totalPrice);

            registrationManager.SaveRegistration(newRegistration);

            MessageBox.Show("Registration has been created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);


            
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelectActivity_button(object sender, RoutedEventArgs e)
        {
            // Create an instance of the new window
            ActivitySelectionWindow activitySelectionWindow = new ActivitySelectionWindow();
            



            if (activitySelectionWindow.ShowDialog() == true)
            {
                selectedActivity = activitySelectionWindow.selectedActivity;
                ActivityNameTextBox.Text = selectedActivity.Name;
            }

        }

        private void MembersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedMembers = MembersListBox.SelectedItems.Cast<Member>().ToList();
           
            totalPrice = 0;

            totalPrice = registrationManager.CalculateTotalPrice(selectedMembers, selectedActivity);

            // Display the total price in the TextBox
            TotalPriceTextBox.Text = totalPrice.ToString("C"); // Assuming TotalPriceTextBox is the name of your TextBox

        }
    }
}
