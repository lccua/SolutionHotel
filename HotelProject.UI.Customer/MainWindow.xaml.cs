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

        public List<Customer> customers;

        public CustomerUI selectedCustomer;

        private ObservableCollection<CustomerUI> customersUIs=new ObservableCollection<CustomerUI>();

        public MainWindow()
        {
            InitializeComponent();
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            customers =  customerManager.GetCustomers(null);

            customersUIs = new ObservableCollection<CustomerUI>(customers.Select(x =>
            {
                var customerUI = CustomerMapper.MapToUI(x);


                // Populate MembersList
                customerUI.MembersList = customerUI.MembersList;

                return customerUI;
            }));


            CustomerDataGrid.ItemsSource = customersUIs;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerDataGrid.ItemsSource = new ObservableCollection<CustomerUI>(customerManager.GetCustomers(SearchTextBox.Text).Select(x => new CustomerUI(x.Id, x.Name, x.ContactInfo.Email, x.ContactInfo.Phone, x.ContactInfo.Address.ToString(), x.GetMembers().Count)));
        }

        private void MenuItemAddCustomer_Click(object sender, RoutedEventArgs e)
        {

            CustomerWindow w = new CustomerWindow(null, customers, false);
            if (w.ShowDialog()==true)
                customersUIs.Add(w.selectedCustomerUI);
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

        private void MenuItemUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Customer not selected", "Update");
                return;
            }

            int selectedIndex = CustomerDataGrid.SelectedIndex;
            selectedCustomer = (CustomerUI)CustomerDataGrid.SelectedItem;

            CustomerWindow w = new CustomerWindow(selectedCustomer, customers, true);
            if (w.ShowDialog() == true)
            {
                // Remove the old customer details from the collection
                customersUIs.RemoveAt(selectedIndex);

                // Add the updated customer details at the same index where the old details were
                customersUIs.Insert(selectedIndex, w.selectedCustomerUI);

                // Refresh the DataGrid to reflect the changes
                CustomerDataGrid.ItemsSource = null;
                CustomerDataGrid.ItemsSource = customersUIs;
            }
        }


    }
}
