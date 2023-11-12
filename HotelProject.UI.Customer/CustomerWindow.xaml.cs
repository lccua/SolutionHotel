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

        public Member newMember = new Member();
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

                c.Members = members;

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
            // Check if any of the textboxes is empty
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(EmailTextBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneTextBox.Text) ||
                string.IsNullOrWhiteSpace(CityTextBox.Text) ||
                string.IsNullOrWhiteSpace(ZipTextBox.Text) ||
                string.IsNullOrWhiteSpace(HouseNumberTextBox.Text) ||
                string.IsNullOrWhiteSpace(StreetTextBox.Text))
            {
                MessageBox.Show("Please fill in all the fields.", "Incomplete Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Stop processing further if any field is empty
            }


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

                newMember.Name = w.memberUI.Name;
                newMember.BirthDay = w.memberUI.Birthday;
                

                c.AddMember(newMember);

                // Update the MemberDataGrid with the new member
                MemberDataGrid.ItemsSource = membersUIs;
            }
               
        }

        private void MenuItemDeleteMember_Click(object sender, RoutedEventArgs e)
        {
            if (MemberDataGrid.SelectedItem == null) MessageBox.Show("Member not selected", "Delete");

            else
            {
                MemberUI selectedMember = (MemberUI)MemberDataGrid.SelectedItem;

                int memberId = selectedMember.Id;
                customerManager.DeleteMember(memberId);

                membersUIs.Remove(selectedMember);
            }

           
        }


    }
}
