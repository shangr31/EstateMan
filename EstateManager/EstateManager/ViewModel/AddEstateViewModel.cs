using EstateManager.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace EstateManager.ViewModel
{
    class AddEstateViewModel : WindowNotifyPropertyChanged
    {
        public ObservableCollection<Person> OwnersList { get { return GetProperty<ObservableCollection<Person>>(); } set { SetProperty(value); } }
        public String Address { get { return GetProperty<String>(); } set { SetProperty(value); } }
        public String ZIP { get { return GetProperty<String>(); } set { SetProperty(value); } }
        public String City { get { return GetProperty<String>(); } set { SetProperty(value); } }
        public String RoomsCount { get { return GetProperty<String>(); } set { SetProperty(value); } }
        public DateTime BuildDate { get { return GetProperty<DateTime>(); } set { SetProperty(value); } }
        public String Floors { get { return GetProperty<String>(); } set { SetProperty(value); } }
        public String FloorNumber { get { return GetProperty<String>(); } set { SetProperty(value); } }
        public String EnergyEfficiency { get { return GetProperty<String>(); } set { SetProperty(value); } }
        public BitmapImage Image { get { return GetProperty<BitmapImage>(); } set { SetProperty(value); } }
        public string Link
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }
        public String Latitude { get { return GetProperty<String>(); } set { SetProperty(value); } }
        public String Longitude { get { return GetProperty<String>(); } set { SetProperty(value); } }
        public String Surface { get { return GetProperty<String>(); } set { SetProperty(value); } }
        public int Uid { get { return GetProperty<int>(); } set { SetProperty(value); } }
        public Person Owner { get { return GetProperty<Person>(); } set { SetProperty(value); } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="view"></param>
        public AddEstateViewModel(Window view) : base(view)
        {
            OwnersList = new ObservableCollection<Person>(DataAccess.EstateDbContext.Current.Persons.ToArray());
            BuildDate = DateTime.Today;
        }



        /// <summary>
        /// Command
        /// </summary>
        public Command getImage
        {
            get
            {
                return new Command(() =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.ShowDialog();
                    try
                    {
                        Link = Base64StringToBitmapImageConverter.BitmapImageToBase64String(new BitmapImage(new Uri(openFileDialog.FileName)));
                    }
                    catch (Exception e)
                    {

                    }

                });
            }
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
                    if (Address != null && Address != "" && City != null && City != "" && ZIP != null && ZIP.Length == 5 && Owner != null && Link != "" && Link != null)
                    {
                        if (Surface == "" || Surface == null)
                            Surface = "0";
                        if (Latitude == "" || Latitude == null)
                            Latitude = "0";
                        if (Longitude == "" || Longitude == null)
                            Longitude = "0";
                        if (RoomsCount == "" || RoomsCount == null)
                            RoomsCount = "0";
                        if (Floors == "" || Floors == null)
                            Floors = "0";
                        if (FloorNumber == "" || FloorNumber == null)
                            FloorNumber = "0";
                        if (EnergyEfficiency == "" || EnergyEfficiency == null)
                            EnergyEfficiency = "0";
                        if (BuildDate == null)
                            BuildDate = DateTime.Today;
                        var c = new Estate()
                        {
                            Type = (Estate.EstateType)(Uid + 1),
                            Surface = float.Parse(Surface),
                            Address = Address,
                            City = City,
                            Zip = ZIP,
                            Latitude = Double.Parse(Latitude),
                            Longitude = Double.Parse(Longitude),
                            RoomsCount = Int32.Parse(RoomsCount),
                            FloorCount = Int32.Parse(Floors),
                            FloorNumber = Int32.Parse(FloorNumber),
                            EnergyEfficiency = Int32.Parse(EnergyEfficiency),
                            OwnerId = Owner.Id,
                            BuildDate = BuildDate

                        };
                        DataAccess.EstateDbContext.Current.Add(c);

                        var photo = new Photo()
                        {
                            Content = Link,
                            Title = "mainPhoto",
                            EstateId = c.Id
                        };
                        DataAccess.EstateDbContext.Current.Add(photo);

                        DataAccess.EstateDbContext.Current.SaveChanges();

                        c.MainPhoto = photo;
                        DataAccess.EstateDbContext.Current.Update(c);
                        DataAccess.EstateDbContext.Current.SaveChanges();
                        

                        Service.NavigationService.Close(this, true);
                    }

                    Service.NavigationService.Close(this, false);






                    //DataAccess.EstateDbContext.Current.SaveChanges();

                });


                //if()
                // var c = new Person()
                // {

                // };
                // Estate c = new Estate(-1,Int32.Parse(Surface),Estate.EstateType.Field, Int32.Parse(RoomsCount), Int32.Parse(Floors),Int32.Parse(FloorNumber),Address,ZIP, City,Double.Parse(Latitude), Double.Parse(Longitude),Int32.Parse(EnnergyEfficiency),0,0);
                //  DataAccess.EstateDbContext.Current.Add(c);

            }
        }
        public Command AddOwner
        {
            get
            {
                return new Command(() =>
                {
                    bool e = (bool)Service.NavigationService.ShowDialog<View.AdOwnerWindow, AddOwnerViewModel>();
                    OwnersList = new ObservableCollection<Person>(DataAccess.EstateDbContext.Current.Persons);
                });
            }
        }

    }
}


