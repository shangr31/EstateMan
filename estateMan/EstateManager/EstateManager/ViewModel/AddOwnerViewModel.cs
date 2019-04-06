using EstateManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EstateManager.ViewModel
{
    class AddOwnerViewModel : WindowNotifyPropertyChanged
    {
        public AddOwnerViewModel(Window v) : base(v)
        {
        }

        public int Uid
        {
            get { return GetProperty<int>(); }
            set { SetProperty(value); }
        }
        public String name
        {
            get { return GetProperty<String>(); }
            set { SetProperty(value); }
        }
        public String firstname
        {
            get { return GetProperty<String>(); }
            set { SetProperty(value); }
        }
        public String address
        {
            get { return GetProperty<String>(); }
            set { SetProperty(value); }
        }
        public String Zip
        {
            get { return GetProperty<String>(); }
            set { SetProperty(value); }
        }
        public String city
        {
            get { return GetProperty<String>(); }
            set { SetProperty(value); }
        }
        public String latitude
        {
            get { return GetProperty<String>(); }
            set { SetProperty(value); }
        }
        public String longitude
        {
            get { return GetProperty<String>(); }
            set { SetProperty(value); }
        }
        public String phone
        {
            get { return GetProperty<String>(); }
            set { SetProperty(value); }
        }
        public String cellPhone
        {
            get { return GetProperty<String>(); }
            set { SetProperty(value); }
        }
        public String Mail
        {
            get { return GetProperty<String>(); }
            set { SetProperty(value); }
        }
        public Command dismiss
        {
            get
            {
                return new Command(() =>
                {
                    Service.NavigationService.Close(this, false);
                });
            }
        }
        public Command validate
        {
            get
            {
                return new Command(() =>
                {
                    if (name != null && name != "" && firstname != null && firstname != "")
                    {
                        if (address == null)
                            address = "Non renseigné";
                        if (Zip == null || Zip.Length != 5)
                            Zip = "Non renseigné";
                        if (city == null)
                            city = "Non renseigné";
                        if (latitude == null)
                            latitude = "0";
                        if (longitude == null)
                            longitude = "0";
                        if (phone == null)
                            phone = "Non renseigné";
                        if (cellPhone == null)
                            cellPhone = "Non renseigné";
                        if (Mail == null)
                            Mail = "Non renseigné";

                        var c = new Person
                        {
                            Quality = (Person.PersonQuality)Uid,
                            Name = name,
                            Firstname = firstname,
                            Address = address,
                            Zip = Zip,
                            City = city,
                            Latitude = Double.Parse(latitude),
                            Longitude = Double.Parse(longitude),
                            Phone = phone,
                            CellPhone = cellPhone,
                            Mail = Mail
                        };
                        DataAccess.EstateDbContext.Current.Add(c);
                        DataAccess.EstateDbContext.Current.SaveChanges();

                        Service.NavigationService.Close(this, true);
                    }
                    Service.NavigationService.Close(this, false);
                });
            }
        }
    }
}
