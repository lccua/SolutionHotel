using HotelProject.BL.Managers;
using HotelProject.Auth;
using HotelProject.BL.Model;
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
using HotelProject.Auth.Service;




namespace HotelProject.UI.OrganizerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        private OrganizerManager organizerManager;
        private PasswordService passwordService;

        public MainWindow()
        {
            InitializeComponent();
            organizerManager = new OrganizerManager(RepositoryFactory.OrganizerRepository);
            passwordService = new PasswordService();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = LoginUsernameTextBox.Text;
            string existingHashedPassword = organizerManager.GetHashedPasswordByUsername(userName);

            if (existingHashedPassword != null)
            {
                string inputPassword = LoginPasswordBox.Password;
                bool isAuthenticated = passwordService.VerifyPassword(existingHashedPassword, inputPassword);

                if (isAuthenticated)
                {
                    // Authentication successful
                    MessageBox.Show("Authentication successful!", "Success");

                    // Open the ManagementWindow
                    ManagementWindow managementWindow = new ManagementWindow();
                    managementWindow.Show();

                    // Optionally, you might want to close the current (Login) window
                    this.Close();
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
