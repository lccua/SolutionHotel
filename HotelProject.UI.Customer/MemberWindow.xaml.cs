using HotelProject.BL.Model;
using HotelProject.UI.CustomerWPF.Model;
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
 
    public partial class MemberWindow : Window
    {
        public MemberUI memberUI;
        public CustomerUI customerUI;

        public MemberWindow(CustomerUI customerUI)
        {
            InitializeComponent();

            if (customerUI == null)
            {
                this.customerUI = new CustomerUI();
                this.customerUI.Members = new List<Member>();
            }
            else
            {
                this.customerUI = customerUI;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            
            DateTime? selectedDate = BirthDayBox.SelectedDate;
            DateOnly dateOnlyBirthday = DateOnly.FromDateTime(selectedDate.Value);

            memberUI = new MemberUI(NameTextBox.Text, dateOnlyBirthday);

            DialogResult = true;
            Close();
        }
    }
}
