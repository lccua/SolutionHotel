﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.UI.OrganizerWPF.Model
{
    public class ActivityUI : INotifyPropertyChanged
    {
        public ActivityUI(int id, string name, DateOnly sheduledDate, int availableSpots, decimal adultPrice, decimal childPrice, int discount, 
                            string description, int duration, string city, int zipCode, string street, string houseNumber)
        {
            this.id = id;
            this.name = name;
            this.sheduledDate = sheduledDate;
            this.availableSpots = availableSpots;
            this.adultPrice = adultPrice;
            this.childPrice = childPrice;
            this.discount = discount;
            this.description = description;
            this.duration = duration;
            this.city = city;
            this.zipCode = zipCode;
            this.street = street;
            this.houseNumber = houseNumber;
         
        }

        private int id;
        public int Id { get { return id; } set { id = value; OnPropertyChanged(); } }

        private string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged(); } }

        private DateOnly sheduledDate;
        public DateOnly SheduledDate { get { return sheduledDate; } set { sheduledDate = value; OnPropertyChanged(); } }

        private int availableSpots;
        public int AvailableSpots { get { return availableSpots; } set { availableSpots = value; OnPropertyChanged(); } }

        private decimal adultPrice;
        public decimal AdultPrice { get { return adultPrice; } set { adultPrice = value; OnPropertyChanged(); } }

        private decimal childPrice;
        public decimal ChildPrice { get { return childPrice; } set { childPrice = value; OnPropertyChanged(); } }

        private int discount;
        public int Discount { get { return discount; } set { discount = value; OnPropertyChanged(); } }

        private string description;
        public string Description { get { return description; } set { description = value; OnPropertyChanged(); } }

        private int duration;
        public int Duration { get { return duration; } set { duration = value; OnPropertyChanged(); } }

        private string city;
        public string City { get { return city; } set { city = value; OnPropertyChanged(); } }

        private int zipCode;
        public int ZipCode { get { return zipCode; } set { zipCode = value; OnPropertyChanged(); } }

        private string street;
        public string Street { get { return street; } set { street = value; OnPropertyChanged(); } }

        private string houseNumber;
        public string HouseNumber { get { return houseNumber; } set { houseNumber = value; OnPropertyChanged(); } }

        private void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}