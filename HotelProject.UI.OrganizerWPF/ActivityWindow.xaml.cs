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
using HotelProject.UI.OrganizerWPF.Model;
using HotelProject.BL.Managers;
using HotelProject.BL.Model;
using HotelProject.Util;

namespace HotelProject.UI.OrganizerWPF
{
    /// <summary>
    /// Interaction logic for ActivityWindow.xaml
    /// </summary>
    public partial class ActivityWindow : Window
    {
        public ActivityUI activityUI;
        private bool isUpdate;

        private ActivityManager activityManager;
        public ActivityWindow(bool isUpdate, ActivityUI activityUI)
        {
            InitializeComponent();
            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);

            this.activityUI = activityUI;
            this.isUpdate = isUpdate;

            if (activityUI != null)
            {
                //change textbox names in wpf xaml
                IdTextBox.Text = activityUI.Id.ToString();
                //add more pls
             

            }
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (isUpdate)
            {
                Activity a = new Activity();

                activityManager.UpdateActivity(a, a.Id);
                //add ID to class Activity


                activityUI.Name = a.Name;
                //add more
            }
            else
            {
                Activity a = new Activity();

                activityManager.AddActivity(a);

                int lastId = activityManager.GetLastActivityId();
                int currentId = lastId;
                a.Id = currentId;

                ActivityUI = new ActivityUI();
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

