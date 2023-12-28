using HotelProject.BL.Managers;
using HotelProject.BL.Model;
using HotelProject.DL.Repositories;
using HotelProject.UI.CustomerWPF.Mapper;
using HotelProject.UI.CustomerWPF.Model;
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

namespace HotelProject.UI.CustomerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CustomerManager customerManager;

        public CustomerUI selectedCustomerUI;
        private ObservableCollection<CustomerUI> customersUIs;

        public MainWindow()
        {
            InitializeComponent();
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);

            customersUIs = new ObservableCollection<CustomerUI>(
                           customerManager.GetCustomers(null).Select(x => CustomerMapper.MapToUI(x)));

            CustomerDataGrid.ItemsSource = customersUIs;

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            customersUIs = new ObservableCollection<CustomerUI>(
                           customerManager.GetCustomers(SearchTextBox.Text).Select(x => CustomerMapper.MapToUI(x)));

            CustomerDataGrid.ItemsSource = customersUIs;
        }

        private void MenuItemDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null) MessageBox.Show("Customer not selected", "Delete");
            else
            {
                CustomerUI selectedCustomer = (CustomerUI)CustomerDataGrid.SelectedItem;
                int customerId = selectedCustomer.Id;
                customerManager.DeleteCustomer(customerId);

                customersUIs.Remove(selectedCustomer);
            }
        }

        private void MenuItemAddCustomer_Click(object sender, RoutedEventArgs e)
        {

            CustomerWindow w = new CustomerWindow(null, false);
            if (w.ShowDialog() == true)
                customersUIs.Add(w.customerUI);
        }

        private void MenuItemUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Customer not selected", "Update");
                return;
            }

            int selectedIndex = CustomerDataGrid.SelectedIndex;
            selectedCustomerUI = (CustomerUI)CustomerDataGrid.SelectedItem;

            CustomerWindow w = new CustomerWindow(selectedCustomerUI, true);
            if (w.ShowDialog() == true)
            {
                // Remove the old customer details from the collection
                customersUIs.RemoveAt(selectedIndex);

                // Refresh the DataGrid to reflect the changes
                customersUIs.Insert(selectedIndex,w.customerUI);
            }
        }
    }
}
