using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateManager.Model
{
    public class Person : BaseNotifyPropertyChanged
    {
        public enum PersonQuality { legal, mister, miss};
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get { return GetProperty<int>(); }
            set { SetProperty(value); }
        }
        public PersonQuality Quality
        {
            get { return GetProperty<PersonQuality>(); }
            set { SetProperty(value); }
        }
        public String Name
        {
            get { return GetProperty<String>(); }
            set { SetProperty(value); }
        }
        public String Firstname
        {
            get { return GetProperty<String>(); }
            set { SetProperty(value); }
        }
        public String Address
        {
            get { return GetProperty<String>(); }
            set { SetProperty(value); }
        }
        public String Zip
        {
            get { return GetProperty<String>(); }
            set { SetProperty(value); }
        }
        public String City
        {
            get { return GetProperty<String>(); }
            set { SetProperty(value); }
        }
        public double? Latitude
        {
            get { return GetProperty<double>(); }
            set { SetProperty(value); }
        }
        public double? Longitude
        {
            get { return GetProperty<double>(); }
            set { SetProperty(value); }
        }
        public String Phone
        {
            get { return GetProperty<String>(); }
            set { SetProperty(value); }
        }
        public String CellPhone
        {
            get { return GetProperty<String>(); }
            set { SetProperty(value); }
        }
        public String Mail
        {
            get { return GetProperty<String>(); }
            set { SetProperty(value); }
        }

        public Person()
        { }
        public Person(int id, PersonQuality quality, string name, string firstname, string address, string zip, string city, double latitude, double longitude, string phone, string cellPhone, string mail)
        {
            Id = id;
            Quality = quality;
            Name = name;
            Firstname = firstname;
            Address = address;
            Zip = zip;
            City = city;
            Latitude = latitude;
            Longitude = longitude;
            Phone = phone;
            CellPhone = cellPhone;
            Mail = mail;
        }

        public override string ToString()
        {
            string retVal = string.Empty;
            retVal = Name + "  " + Firstname;
            return retVal;
        }
    }
}
