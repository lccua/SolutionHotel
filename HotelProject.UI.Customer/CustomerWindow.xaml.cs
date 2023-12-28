using HotelProject.BL.Managers;
using HotelProject.BL.Model;
using HotelProject.UI.CustomerWPF.Mapper;
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
        private bool IsUpdate = false;

        public CustomerUI customerUI;
        private Customer customer = new Customer();

        private ObservableCollection<MemberUI> membersUIs = new ObservableCollection<MemberUI>();
        public List<Member> members;

        #region Initialization and Setup

        public CustomerWindow(CustomerUI selectedCustomerUI,bool isUpdate)
        {
            InitializeComponent();
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);

            this.customerUI = selectedCustomerUI;

            this.IsUpdate = isUpdate;
      
            if (IsUpdate)
            {
              
                members = selectedCustomerUI.Members;

                membersUIs = new ObservableCollection<MemberUI>(members.Select(x => MemberMapper.MapToUI(x)));
                MemberDataGrid.ItemsSource = membersUIs;

                customer.Members.Clear();

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

        #region Menu Add and Delete Member Buttons

        private void MenuItemAddMember_Click(object sender, RoutedEventArgs e)
        {
            if (customerUI == null)
            {
                customerUI = new CustomerUI();
                customerUI.Members = new List<Member>();
            }

            MemberWindow w = new MemberWindow(customerUI);

            if (w.ShowDialog() == true)
            {
                
                Member newMember = new Member(w.memberUI.Name, w.memberUI.Birthday);


                membersUIs.Add(w.memberUI);

               
                customer.Members.Add(newMember);

                MemberDataGrid.ItemsSource = membersUIs;

            }

        }

        private void MenuItemDeleteMember_Click(object sender, RoutedEventArgs e)
        {
            if (MemberDataGrid.SelectedItem == null) MessageBox.Show("Member not selected", "Delete");

            else
            {
                MemberUI selectedMemberUI = (MemberUI)MemberDataGrid.SelectedItem;
                Member member = MemberMapper.MapToDomain(selectedMemberUI);

                int memberId = selectedMemberUI.Id;
                customerManager.DeleteMember(memberId);

                membersUIs.Remove(selectedMemberUI);

                customer.Members.Remove(member);

            }


        }

        #endregion

        #region Customer Update and Add Methods

        private void UpdateNewCustomer()
        {
            customer.Name = NameTextBox.Text;
            customer.Id = int.Parse(IdTextBox.Text);

            customer.ContactInfo = new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, new Address(CityTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text, StreetTextBox.Text));

            customerManager.UpdateCustomer(customer, customer.Id);


            customerUI = CustomerMapper.MapToUI(customer);
            customerUI.Members.Clear();
            customerUI.Members.AddRange(membersUIs.Select(x => MemberMapper.MapToDomain(x)));

            customerUI.NrOfMembers = membersUIs.Count;


        }

        private void AddNewCustomer()
        {
            customer.Name = NameTextBox.Text;
            customer.ContactInfo = new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, new Address(CityTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text, StreetTextBox.Text));

            int currentId = customerManager.AddCustomer(customer);
            customer.Id = currentId;


            customerUI = CustomerMapper.MapToUI(customer);
            customerUI.Members.Clear();
            customerUI.Members.AddRange(membersUIs.Select(x => MemberMapper.MapToDomain(x)));

            customerUI.NrOfMembers = membersUIs.Count;


        }

        #endregion

        #region Customer Window Cancel Button

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion

    }
}
