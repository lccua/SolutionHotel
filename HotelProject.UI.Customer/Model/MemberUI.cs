using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.UI.CustomerWPF.Model
{
    public class MemberUI : INotifyPropertyChanged
    {
        public MemberUI(int id, string name, DateOnly birthday)
        {
            this.id = id;
            this.name = name;
            this.birthday = birthday;   
        }

        public MemberUI(string name, DateOnly birthday)
        {
            this.name = name;
            this.birthday = birthday;
        }


        private int id;
        public int Id { get { return id; } set { id = value; OnPropertyChanged(); } }

        private string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged(); } }

        private DateOnly birthday;
        public DateOnly Birthday { get { return birthday; } set { birthday = value; OnPropertyChanged(); } }

        private void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
