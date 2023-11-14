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
        public CustomerUI selectedCustomerUI;

        private bool IsUpdate => selectedCustomerUI != null;



       
        public List<Member> selectedCustomerMemberList;

        public Member newMember = new Member();
        private List<Customer> customers; // Add this field

        private ObservableCollection<MemberUI> membersUIs = new ObservableCollection<MemberUI>();

        private Customer c = new Customer();


        private CustomerManager customerManager;
        public CustomerWindow(CustomerUI selectedCustomerUI, List<Customer> customers)
        {
            InitializeComponent();
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);

            this.customers = customers; // Store the customers list locally

            this.selectedCustomerUI = selectedCustomerUI;
      

           
         

           

            if (IsUpdate)
            {
              
                selectedCustomerMemberList = selectedCustomerUI.MembersList;

                membersUIs = new ObservableCollection<MemberUI>(selectedCustomerMemberList.Select(x => new MemberUI(x.Id, x.Name, x.BirthDay)));
                MemberDataGrid.ItemsSource = membersUIs;

                selectedCustomerMemberList.Clear();

                IdTextBox.Text = selectedCustomerUI.Id.ToString();
                NameTextBox.Text = selectedCustomerUI.Name;
                EmailTextBox.Text = selectedCustomerUI.Email;
                PhoneTextBox.Text = selectedCustomerUI.Phone;

               

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


            if (IsUpdate)
            {
                
                c.Name = NameTextBox.Text;
                c.Id = int.Parse(IdTextBox.Text);

                c.ContactInfo = new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, new Address(CityTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text, StreetTextBox.Text));

                
              
            
                customerManager.UpdateCustomer(c, selectedCustomerUI.Id);

                selectedCustomerMemberList =  MemberDataGrid.ItemsSource.Cast<MemberUI>()
                                               .Select(x => new Member(x.Id,x.Name,x.Birthday))
                                               .ToList();

                selectedCustomerUI = new CustomerUI(c.Id, c.Name, c.ContactInfo.Email, c.ContactInfo.Phone, c.ContactInfo.Address.ToString(), membersUIs.Count());
                selectedCustomerUI.MembersList = selectedCustomerMemberList;

            }
            else
            {
                c.Name = NameTextBox.Text;
                c.ContactInfo = new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, new Address(CityTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text, StreetTextBox.Text));

                int currentId = customerManager.AddCustomer(c);
                c.Id = currentId;

                selectedCustomerUI = new CustomerUI(c.Id, c.Name, c.ContactInfo.Email, c.ContactInfo.Phone, c.ContactInfo.Address.ToString(), c.GetMembers().Count);
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
            MemberWindow w = new MemberWindow(selectedCustomerUI);

            if (w.ShowDialog() == true)
            {
                membersUIs.Add(w.memberUI);

                newMember.Name = w.memberUI.Name;
                newMember.BirthDay = w.memberUI.Birthday;
                
                selectedCustomerUI.MembersList.Add(newMember);

                

                // Update the MemberDataGrid with the new member
                MemberDataGrid.ItemsSource = membersUIs;

                c.AddMember(newMember);
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
