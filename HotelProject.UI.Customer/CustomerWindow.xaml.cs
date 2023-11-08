using HotelProject.BL.Managers;
using HotelProject.BL.Model;
using HotelProject.UI.CustomerWPF.Model;
using HotelProject.Util;
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
        public List<Member> members;
        private List<Customer> customers; // Add this field

        private ObservableCollection<MemberUI> membersUIs = new ObservableCollection<MemberUI>();

        private Customer c = new Customer();


        private CustomerManager customerManager;
        public CustomerWindow(bool isUpdate, CustomerUI customerUI, List<Customer> customers)
        {
            InitializeComponent();
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);

            this.customers = customers; // Store the customers list locally

            

            this.customerUI = customerUI;
            this.isUpdate = isUpdate;

           
         

           

            if (customerUI != null )
            {
                members = customers.Find(cust => cust.Id == customerUI.Id)?.GetMembers();

                membersUIs = new ObservableCollection<MemberUI>(members.Select(x => new MemberUI(x.Id, x.Name, x.BirthDay)));
                MemberDataGrid.ItemsSource = membersUIs;

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
                
                c.Name = NameTextBox.Text;
                c.ContactInfo = new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, new Address(CityTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text, StreetTextBox.Text));

                
                customerManager.UpdateCustomer(c, customerUI.Id);


                customerUI = new CustomerUI(c.Id, c.Name, c.ContactInfo.Email, c.ContactInfo.Phone, c.ContactInfo.Address.ToString(), c.GetMembers().Count);
                customers.Add(c);

            }
            else
            {
                c.Name = NameTextBox.Text;
                c.ContactInfo = new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, new Address(CityTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text, StreetTextBox.Text));

                int currentId = customerManager.AddCustomer(c);
                c.Id = currentId;

                customerUI = new CustomerUI(c.Id, c.Name, c.ContactInfo.Email, c.ContactInfo.Phone, c.ContactInfo.Address.ToString(), c.GetMembers().Count);
                customers.Add(c);
            }
        
            DialogResult = true;
            Close();
        }

       

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItemAddMember_Click(object sender, RoutedEventArgs e)
        {
            MemberWindow w = new MemberWindow(customerUI);

            if (w.ShowDialog() == true)
            {
                membersUIs.Add(w.memberUI);
                Member newMember = new Member(w.memberUI.Name, w.memberUI.Birthday);
                c.AddMember(newMember);

                // Update the MemberDataGrid with the new member
                MemberDataGrid.ItemsSource = membersUIs;
            }
               
        }

        private void MenuItemDeleteMember_Click(object sender, RoutedEventArgs e)
        {
            MemberUI selectedMember = (MemberUI)MemberDataGrid.SelectedItem;

            int memberId = selectedMember.Id;
            customerManager.DeleteMember(memberId);

            membersUIs.Remove(selectedMember);
        }


    }
}
