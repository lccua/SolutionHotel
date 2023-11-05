using HotelProject.BL.Managers;
using HotelProject.BL.Model;
using HotelProject.UI.CustomerWPF.Model;
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

namespace HotelProject.UI.CustomerWPF
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public CustomerUI customerUI;
        private bool isUpdate;

        private CustomerManager customerManager;
        public CustomerWindow(bool isUpdate,CustomerUI customerUI)
        {
            InitializeComponent();
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);

            this.customerUI = customerUI;
            this.isUpdate = isUpdate;

            if (customerUI != null )
            {
                IdTextBox.Text = customerUI.Id.ToString();
                NameTextBox.Text = customerUI.Name;
                EmailTextBox.Text = customerUI.Email;
                PhoneTextBox.Text = customerUI.Phone;
                
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (isUpdate)
            {
                Customer c = new Customer(NameTextBox.Text, new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, new Address(CityTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text, StreetTextBox.Text)));

                customerManager.UpdateCustomer(c, customerUI.Id);

              
                customerUI.Name = c.Name;
                customerUI.Email = c.ContactInfo.Email;
                customerUI.Phone = c.ContactInfo.Phone;
                customerUI.Address = c.ContactInfo.Address.ToString();
                customerUI.NrOfMembers = c.GetMembers().Count;
            }
            else
            {
                Customer c = new Customer(NameTextBox.Text, new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, new Address(CityTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text, StreetTextBox.Text)));
                
                customerManager.AddCustomer(c);

                int lastId = customerManager.GetLastCustomerId();
                int currentId = lastId;
                c.Id = currentId;

                customerUI = new CustomerUI(c.Id, c.Name, c.ContactInfo.Email, c.ContactInfo.Phone, c.ContactInfo.Address.ToString(), c.GetMembers().Count);
            }
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
