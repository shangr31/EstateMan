using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstateManager.Model;

namespace EstateManager.ViewModel
{
    public class HomeViewModel : BaseNotifyPropertyChanged
    {
        public HomeViewModel()
        {
            LoadColumnChartData();
        }
        
        public KeyValuePair<String, double>[] EstateTypeSeries
        {
            get { return GetProperty<KeyValuePair<String, double>[]>(); }
            set { SetProperty(value);  }
        }
        
        public KeyValuePair<String, double>[] ContractTypeSeries
        {
            get { return GetProperty<KeyValuePair<String, double>[]>(); }
            set { SetProperty(value); }
        }

        public KeyValuePair<String, double>[] RentalEstateTypeSeries
        {
            get { return GetProperty<KeyValuePair<String, double>[]>(); }
            set { SetProperty(value); }
        }

        public KeyValuePair<String, double>[] SaleEstateTypeSeries
        {
            get { return GetProperty<KeyValuePair<String, double>[]>(); }
            set { SetProperty(value); }
        }

        private void LoadColumnChartData()
        {
            

            //PIE CHAT ON ESTATE TYPE
            KeyValuePair<String, double>[] EstateType = new KeyValuePair<String, double>[6];

            int flatCount = DataAccess.EstateDbContext.Current.Estates.Where(e => e.Type == Estate.EstateType.Flat).Count();
            int houseCount = DataAccess.EstateDbContext.Current.Estates.Where(e => e.Type == Estate.EstateType.House).Count();
            int garageCount = DataAccess.EstateDbContext.Current.Estates.Where(e => e.Type == Estate.EstateType.Garage).Count();
            int fieldCount = DataAccess.EstateDbContext.Current.Estates.Where(e => e.Type == Estate.EstateType.Field).Count();
            int commercialLocalCount = DataAccess.EstateDbContext.Current.Estates.Where(e => e.Type == Estate.EstateType.CommercialLocal).Count();
            int othersCount = DataAccess.EstateDbContext.Current.Estates.Where(e => e.Type == Estate.EstateType.Others).Count();

            EstateType[0] = new KeyValuePair<String, double>("Flat", flatCount);
            EstateType[1] = new KeyValuePair<String, double>("House", houseCount);
            EstateType[2] = new KeyValuePair<String, double>("Garage", garageCount);
            EstateType[3] = new KeyValuePair<String, double>("Field", fieldCount);
            EstateType[4] = new KeyValuePair<String, double>("Commercial Local", commercialLocalCount);
            EstateType[5] = new KeyValuePair<String, double>("Others", othersCount);

            EstateTypeSeries = EstateType;

            //PIE CHART ON CONTRACT TYPE
            KeyValuePair<String, double>[] ContractType = new KeyValuePair<String, double>[2];

            int rentalCount = DataAccess.EstateDbContext.Current.Contracts.Where(e => e.Type == Contract.ContractType.Rental).Count();
            int saleCount = DataAccess.EstateDbContext.Current.Contracts.Where(e => e.Type == Contract.ContractType.Sale).Count();

            ContractType[0] = new KeyValuePair<String, double>("Rental", rentalCount);
            ContractType[1] = new KeyValuePair<String, double>("Sale", saleCount);

            ContractTypeSeries = ContractType;

            //AREA CHART CONTRACT TYPE BY ESTATE TYPE
            //ESTATE TYPE ON RENTAL
            KeyValuePair<String, double>[] RentalEstateType = new KeyValuePair<String, double>[6];

            int FlatRentalCount = DataAccess.EstateDbContext.Current.Estates.Where(e => e.Type == Estate.EstateType.Flat && e.Contracts.Where(c => c.Type == Contract.ContractType.Rental).Count() >= 1).Count();
            int HouseRentalCount = DataAccess.EstateDbContext.Current.Estates.Where(e => e.Type == Estate.EstateType.House && e.Contracts.Where(c => c.Type == Contract.ContractType.Rental).Count() >= 1).Count();
            int GarageRentalCount = DataAccess.EstateDbContext.Current.Estates.Where(e => e.Type == Estate.EstateType.Garage && e.Contracts.Where(c => c.Type == Contract.ContractType.Rental).Count() >= 1).Count();
            int FieldRentalCount = DataAccess.EstateDbContext.Current.Estates.Where(e => e.Type == Estate.EstateType.Field && e.Contracts.Where(c => c.Type == Contract.ContractType.Rental).Count() >= 1).Count();
            int CommercialLocalRentalCount = DataAccess.EstateDbContext.Current.Estates.Where(e => e.Type == Estate.EstateType.CommercialLocal && e.Contracts.Where(c => c.Type == Contract.ContractType.Rental).Count() >= 1).Count();
            int OthersRentalCount = DataAccess.EstateDbContext.Current.Estates.Where(e => e.Type == Estate.EstateType.Others && e.Contracts.Where(c => c.Type == Contract.ContractType.Rental).Count() >= 1).Count();

            RentalEstateType[0] = new KeyValuePair<String, double>("Flat", FlatRentalCount);
            RentalEstateType[1] = new KeyValuePair<String, double>("House", HouseRentalCount);
            RentalEstateType[2] = new KeyValuePair<String, double>("Garage", GarageRentalCount);
            RentalEstateType[3] = new KeyValuePair<String, double>("Field", FieldRentalCount);
            RentalEstateType[4] = new KeyValuePair<String, double>("CommercialLocal", CommercialLocalRentalCount);
            RentalEstateType[5] = new KeyValuePair<String, double>("Others", OthersRentalCount);

            RentalEstateTypeSeries = RentalEstateType;

            //ESTATE TYPE ON SALE
            KeyValuePair<String, double>[] SaleEstateType = new KeyValuePair<String, double>[6];

            int FlatSaleCount = DataAccess.EstateDbContext.Current.Estates.Where(e => e.Type == Estate.EstateType.Flat && e.Contracts.Where(c => c.Type == Contract.ContractType.Sale).Count() >= 1).Count();
            int HouseSaleCount = DataAccess.EstateDbContext.Current.Estates.Where(e => e.Type == Estate.EstateType.House && e.Contracts.Where(c => c.Type == Contract.ContractType.Sale).Count() >= 1).Count();
            int GarageSaleCount = DataAccess.EstateDbContext.Current.Estates.Where(e => e.Type == Estate.EstateType.Garage && e.Contracts.Where(c => c.Type == Contract.ContractType.Sale).Count() >= 1).Count();
            int FieldSaleCount = DataAccess.EstateDbContext.Current.Estates.Where(e => e.Type == Estate.EstateType.Field && e.Contracts.Where(c => c.Type == Contract.ContractType.Sale).Count() >= 1).Count();
            int CommercialLocalSaleCount = DataAccess.EstateDbContext.Current.Estates.Where(e => e.Type == Estate.EstateType.CommercialLocal && e.Contracts.Where(c => c.Type == Contract.ContractType.Sale).Count() >= 1).Count();
            int OthersSaleCount = DataAccess.EstateDbContext.Current.Estates.Where(e => e.Type == Estate.EstateType.Others && e.Contracts.Where(c => c.Type == Contract.ContractType.Sale).Count() >= 1).Count();

            SaleEstateType[0] = new KeyValuePair<String, double>("Flat", FlatRentalCount);
            SaleEstateType[1] = new KeyValuePair<String, double>("House", HouseRentalCount);
            SaleEstateType[2] = new KeyValuePair<String, double>("Garage", GarageRentalCount);
            SaleEstateType[3] = new KeyValuePair<String, double>("Field", FieldRentalCount);
            SaleEstateType[4] = new KeyValuePair<String, double>("CommercialLocal", CommercialLocalRentalCount);
            SaleEstateType[5] = new KeyValuePair<String, double>("Others", OthersRentalCount);

            SaleEstateTypeSeries = SaleEstateType;


        }
    }
}
