﻿using HotelProject.UI.CustomerWPF.Model;
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
    /// Interaction logic for MemberWindow.xaml
    /// </summary>
    public partial class MemberWindow : Window
    {
        public MemberUI memberUI;

        public MemberWindow(CustomerUI customerUI)
        {
            InitializeComponent();
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