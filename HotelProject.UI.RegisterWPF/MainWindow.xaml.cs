using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using HotelProject.UI.CustomerWPF.Model;
using HotelProject.Util;

namespace HotelProject.UI.RegisterWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CustomerManager customerManager;
        public List<Customer> customers;

        public MainWindow()
        {
            InitializeComponent();
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            customers = customerManager.GetCustomers(null);

            
            // Bind the ComboBox to the list of customer names

            foreach (Customer customer in customers)
            {
                CustomerComboBox.Items.Add(customer);
            }
            

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if an item is selected in the ComboBox
            if (CustomerComboBox.SelectedItem != null)
            {
                // Cast the selected item to a Customer object
                Customer selectedCustomer = (Customer)CustomerComboBox.SelectedItem;

                // Create an instance of the new window and pass the selectedCustomer as a parameter
                RegistrationWindow registrationWindow = new RegistrationWindow(selectedCustomer);

                // Show the new window
                registrationWindow.Show();
            }
            else
            {
                // Handle the case where no item is selected in the ComboBox
                MessageBox.Show("Please select a customer before proceeding.");
            }
        }

    }
}
