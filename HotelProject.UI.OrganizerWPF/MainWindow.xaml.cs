using HotelProject.BL.Managers;
using HotelProject.BL.Model;
using HotelProject.BL.Security;
using HotelProject.DL.Repositories;
using HotelProject.UI.OrganizerWPF.Model;
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




namespace HotelProject.UI.OrganizerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        private OrganizerManager organizerManager;
        private PasswordSecurity passwordSecurity;

        public MainWindow()
        {
            InitializeComponent();
            organizerManager = new OrganizerManager(RepositoryFactory.OrganizerRepository);


        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = LoginUsernameTextBox.Text;
           

            string existingHashedPassword = organizerManager.GetHashedPasswordByUsername(userName);

            if (existingHashedPassword != null)
            {
                string inputPassword = LoginPasswordBox.Password;
                bool isAuthenticated = passwordSecurity.VerifyPassword(existingHashedPassword, inputPassword);

                if (isAuthenticated)
                {
                    // Authentication successful
                    MessageBox.Show("Authentication successful!", "Success");
                }
                else
                {
                    // Authentication failed
                    MessageBox.Show("Authentication failed. Incorrect password or user.", "Error");
                }
            }
            else
            {
                // User not found
                MessageBox.Show("Authentication failed. Incorrect password or user.", "Error");
            }

        }


    }
}
