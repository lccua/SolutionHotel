using HotelProject.BL.Managers;
using HotelProject.BL.Model;
using HotelProject.UI.CustomerWPF.Model;
using HotelProject.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
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

        private CustomerManager customerManager;
        private bool IsUpdate => selectedCustomerUI != null;

        public CustomerUI selectedCustomerUI;

        public List<Member> selectedCustomerMemberList;

        public Member newMember = new Member();
        private List<Customer> customers; // Add this field

        private ObservableCollection<MemberUI> membersUIs = new ObservableCollection<MemberUI>();

        private Customer c = new Customer();


        

        #region Initialization and Setup

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

        #endregion

        #region Customer Add Button

        private bool IsAnyFieldEmpty() => new[] { NameTextBox, EmailTextBox, PhoneTextBox, CityTextBox, ZipTextBox, HouseNumberTextBox, StreetTextBox }.Any(tb => string.IsNullOrWhiteSpace(tb.Text));

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if any of the textboxes is empty, if so show message box and stop further processing
            if (IsAnyFieldEmpty()) { MessageBox.Show("Please fill in all the fields.", "Incomplete Information", 
                                     MessageBoxButton.OK, MessageBoxImage.Warning);
                                     return; }

            if (IsUpdate) { UpdateNewCustomer(); }

            else { AddNewCustomer(); }
        
            DialogResult = true;
            Close();
        }

        #endregion

        #region Customer Window Cancel Button

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion

        #region Menu Add and Delete Member Buttons

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

        #endregion

        #region Customer Update and Addition Methods

        private void UpdateNewCustomer()
        {
            c.Name = NameTextBox.Text;
            c.Id = int.Parse(IdTextBox.Text);

            c.ContactInfo = new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, new Address(CityTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text, StreetTextBox.Text));




            customerManager.UpdateCustomer(c, selectedCustomerUI.Id);

            selectedCustomerMemberList = MemberDataGrid.ItemsSource.Cast<MemberUI>()
                                           .Select(x => new Member(x.Id, x.Name, x.Birthday))
                                           .ToList();

            selectedCustomerUI = new CustomerUI(c.Id, c.Name, c.ContactInfo.Email, c.ContactInfo.Phone, c.ContactInfo.Address.ToString(), membersUIs.Count());
            selectedCustomerUI.MembersList = selectedCustomerMemberList;
        }

        private void AddNewCustomer()
        {
            c.Name = NameTextBox.Text;
            c.ContactInfo = new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, new Address(CityTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text, StreetTextBox.Text));

            int currentId = customerManager.AddCustomer(c);
            c.Id = currentId;

            selectedCustomerUI = new CustomerUI(c.Id, c.Name, c.ContactInfo.Email, c.ContactInfo.Phone, c.ContactInfo.Address.ToString(), c.GetMembers().Count);
            customers.Add(c);
        }

        #endregion

    }
}
