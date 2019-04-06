using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateManager.Model
{
    
    public class Estate : BaseNotifyPropertyChanged
    {
        public enum EstateType{House = 1, Flat = 2, Garage = 3, Field = 4, CommercialLocal = 5, Others = 6};

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get { return GetProperty<int>(); }
            set { SetProperty(value); }
        }
        public float Surface
        {
            get { return GetProperty<float>(); }
            set { SetProperty(value); }
        }
        public EstateType Type
        {
            get { return GetProperty<EstateType>(); }
            set { SetProperty(value); }
        }
        public int RoomsCount
        {
            get { return GetProperty<int>(); }
            set { SetProperty(value); }
        }
        public DateTime? BuildDate
        {
            get { return GetProperty<DateTime>(); }
            set { SetProperty(value); }
        }
        public int FloorCount
        {
            get { return GetProperty<int>(); }
            set { SetProperty(value); }
        }
        public int FloorNumber
        {
            get { return GetProperty<int>(); }
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
        public int EnergyEfficiency
        {
            get { return GetProperty<int>(); }
            set { SetProperty(value); }
        }

        
        public int? MainPhotoId
        {
            get { return GetProperty<int?>(); }
            set { SetProperty(value); }
        }
        [ForeignKey(nameof(MainPhotoId))]
        public Photo MainPhoto
        {
            get { return GetProperty<Photo>(); }
            set { SetProperty(value); }
        }

        [InverseProperty(nameof(Photo.estate))]
        public ObservableCollection<Photo> Photos
        {
            get;
            private set;
        }
        public int OwnerId
        {
            get { return GetProperty<int>(); }
            set { SetProperty(value); }
        }
        [ForeignKey(nameof(OwnerId))]
        public Person Owner
        {
            get { return GetProperty<Person>(); }
            set { SetProperty(value); }
        }

        [InverseProperty(nameof(Contract.estate))]
        public ObservableCollection<Contract> Contracts
        {
            get;
            private set;
        }

        [NotMapped]
        public decimal? CurrentPrice
        {
            get {
                if(Contracts?.Count > 0)
                    return Contracts.Where(c => c.CloseDate == null).FirstOrDefault()?.Price;
                return null;
            }
        }

        public Estate(int id, float surface, EstateType type, int roomsCount, int floorCount, int floorNumber, string address, string zip, string city, double latitude, double longitude, int energyEfficiency, int mainPhotoId, int ownerId)
        {
            Id = id;
            Surface = surface;
            Type = type;
            RoomsCount = roomsCount;
            FloorCount = floorCount;
            FloorNumber = floorNumber;
            Address = address;
            Zip = zip;
            City = city;
            Latitude = latitude;
            Longitude = longitude;
            EnergyEfficiency = energyEfficiency;
            MainPhotoId = mainPhotoId;
            OwnerId = ownerId;
        }

        public Estate()
        {
        }
    }
}
