using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using EstateManager.ViewModel;
using EstateManager.Model;
using System.Windows.Controls;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Globalization;
using EstateManager.View;

namespace EstateManager.ViewModel
{
    public class EstateListManager : BaseNotifyPropertyChanged
    {
        public IQueryable<Estate> EstateList
        {
            get
            {
                return DataAccess.EstateDbContext.Current.Estates.Include(e => e.Contracts);
            }
        }      

        public ObservableCollection<Estate> FilteredEstateList
        {
            get
            {
                var l = EstateList;
                if (!noSelectionET)
                {
                    if (isFlat)
                        l = l.Where(e => e.Type == Estate.EstateType.Flat);
                    if (isHouse)
                        l = l.Where(e => e.Type == Estate.EstateType.House);
                    if (isGarage)
                        l = l.Where(e => e.Type == Estate.EstateType.Garage);
                    if (isField)
                        l = l.Where(e => e.Type == Estate.EstateType.Field);
                    if (isCommercialLocal)
                        l = l.Where(e => e.Type == Estate.EstateType.CommercialLocal);
                    if (isOthers)
                        l = l.Where(e => e.Type == Estate.EstateType.Others);
                }
                if (!noSelectionCT)
                {
                    if (isRental)
                    {                        
                        l = l.Where(e => e.Contracts.Where(c => c.CloseDate == null && c.Type == Contract.ContractType.Rental).Count() >= 1);
                    }
                    if (isSale)
                    {
                        l = l.Where(e => e.Contracts.Where(c => c.CloseDate == null && c.Type == Contract.ContractType.Sale).Count() >= 1);
                    }
                }
                if (!noSelectionCP)
                {
                    if (isOffer)
                    {
                        l = l.Where(e => e.Contracts.Where(c => c.CloseDate == null && c.SignDate == null && c.PubDate != null).Count() >= 1);
                    }
                    if (isSigned)
                    {
                        l = l.Where(e => e.Contracts.Where(c => c.CloseDate == null && c.SignDate != null && c.PubDate != null).Count() >= 1);
                    }
                }
                var list = sortList(l);
                return new ObservableCollection<Estate>(list);
            }
            set { SetProperty(value); }
        }

        public Estate CurrentEstate
        {
            get {
                return GetProperty<Estate>();
            }
            set {
                SetProperty(value);
                DeleteSelectedItem.CanExecute(canBeDelete());
                if (CurrentEstate != null)                
                    FormatedBuildDate = CurrentEstate.BuildDate?.ToString("dd-MM-yyyy");                
                else                
                    FormatedBuildDate = null;                
            }
        }

        public string FormatedBuildDate
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        public EstateListManager()
        {
            noSelectionET = true;
            noSelectionCT = true;
            noSelectionCP = true;
            isPrice = true;
        }

        //RADIO BUTTON FILTERS (ESTATE TYPE)
        public bool noSelectionET
        {
            get { return GetProperty<bool>(); }
            set
            {
                if (SetProperty(value))
                {
                    OnPropertyChanged(nameof(FilteredEstateList));
                }

            }
        }

        public bool isFlat
        {
            get { return GetProperty<bool>(); }
            set
            {
                if (SetProperty(value))
                {
                    OnPropertyChanged(nameof(FilteredEstateList));
                }

            }
        }

        public bool isHouse
        {
            get { return GetProperty<bool>(); }
            set
            {
                if (SetProperty(value))
                {
                    OnPropertyChanged(nameof(FilteredEstateList));
                }

            }
        }

        public bool isGarage
        {
            get { return GetProperty<bool>(); }
            set
            {
                if (SetProperty(value))
                {
                    OnPropertyChanged(nameof(FilteredEstateList));
                }

            }
        }

        public bool isField
        {
            get { return GetProperty<bool>(); }
            set
            {
                if (SetProperty(value))
                {
                    OnPropertyChanged(nameof(FilteredEstateList));
                }

            }
        }

        public bool isCommercialLocal
        {
            get { return GetProperty<bool>(); }
            set
            {
                if (SetProperty(value))
                {
                    OnPropertyChanged(nameof(FilteredEstateList));
                }

            }
        }
        public bool isOthers
        {
            get { return GetProperty<bool>(); }
            set
            {
                if (SetProperty(value))
                {
                    OnPropertyChanged(nameof(FilteredEstateList));
                }

            }
        }

        //RADIO BUTTON FILTERS (CONTRACT TYPE)
        public bool noSelectionCT
        {
            get { return GetProperty<bool>(); }
            set
            {
                if (SetProperty(value))
                {
                    OnPropertyChanged(nameof(FilteredEstateList));
                }

            }
        }

        public bool isSale
        {
            get { return GetProperty<bool>(); }
            set
            {
                if (SetProperty(value))
                {
                    OnPropertyChanged(nameof(FilteredEstateList));
                }

            }
        }

        public bool isRental
        {
            get { return GetProperty<bool>(); }
            set
            {
                if (SetProperty(value))
                {
                    OnPropertyChanged(nameof(FilteredEstateList));
                }

            }
        }

        //RADIO BUTTON FILTERS (CONTRACT PENDING ?)
        public bool noSelectionCP
        {
            get { return GetProperty<bool>(); }
            set
            {
                if (SetProperty(value))
                {
                    OnPropertyChanged(nameof(FilteredEstateList));
                }

            }
        }

        public bool isOffer
        {
            get { return GetProperty<bool>(); }
            set
            {
                if (SetProperty(value))
                {
                    OnPropertyChanged(nameof(FilteredEstateList));
                }

            }
        }

        public bool isSigned
        {
            get { return GetProperty<bool>(); }
            set
            {
                if (SetProperty(value))
                {
                    OnPropertyChanged(nameof(FilteredEstateList));
                }

            }
        }

        //RADIO BUTTON SORTERS
        public bool isPrice
        {
            get { return GetProperty<bool>(); }
            set
            {
                if (SetProperty(value))
                {
                    OnPropertyChanged(nameof(FilteredEstateList));
                }
            }
        }
        public bool isSurface
        {
            get { return GetProperty<bool>(); }
            set
            {
                if (SetProperty(value))
                {
                    OnPropertyChanged(nameof(FilteredEstateList));
                }
            }
        }
        public bool isCity
        {
            get { return GetProperty<bool>(); }
            set
            {
                if (SetProperty(value))
                {
                    OnPropertyChanged(nameof(FilteredEstateList));
                }
            }
        }
        public bool isRooms
        {
            get { return GetProperty<bool>(); }
            set
            {
                if (SetProperty(value))
                {
                    OnPropertyChanged(nameof(FilteredEstateList));
                }
            }
        }
        public bool isEnergyEfficiency
        {
            get { return GetProperty<bool>(); }
            set
            {
                if (SetProperty(value))
                {
                    OnPropertyChanged(nameof(FilteredEstateList));
                }
            }
        }

        //LIST SORT
        private IEnumerable<Estate> sortList(IQueryable<Estate> list)
        {
            IEnumerable<Estate> l = null;
            if (isPrice)
            {
                var li = list.ToList();
                li.Sort((a, b) =>
                {
                    if (a.CurrentPrice == b.CurrentPrice)
                        return 0;
                    else if (a.CurrentPrice == null)
                        return -1;
                    else if (b.CurrentPrice == null)
                        return 1;
                    else
                        return a.CurrentPrice.Value.CompareTo(b.CurrentPrice.Value);
                });
                l = li;
            }
            if (isSurface)
            {
                l = list.OrderBy(e => e.Surface);
            }
            if (isCity)
            {
                l = list.OrderBy(e => e.City);
            }
            if (isRooms)
            {
                l = list.OrderBy(e => e.RoomsCount);
            }
            if (isEnergyEfficiency)
            {
                l = list.OrderBy(e => e.EnergyEfficiency);
            }
            return l;
        }        


        //COMMANDE      


            //DELETE
        public Command DeleteSelectedItem
        {
            get { return new Command(deleteItem, () => { return true; }); }
        }

        private void deleteItem()
        {
            if (canBeDelete())
            {
                DataAccess.EstateDbContext.deleteContractsOf(CurrentEstate);
                DataAccess.EstateDbContext.Current.Estates.Remove(CurrentEstate);
                CurrentEstate = null;
                DataAccess.EstateDbContext.Current.SaveChanges();
                
            }
        }
        private bool canBeDelete()
        {
            if (FilteredEstateList.Count() > 0 && CurrentEstate != null)
                return true;
            return false;

        }


            //OPEN ADD ESTATE WINDOW
        public Command AddEstateButton
        {
            get { return new Command(openWindowAddEstate, () => { return true; }); }
        }

        private void openWindowAddEstate()
        {
           Service.NavigationService.ShowDialog<AddEstateWindow, AddEstateViewModel>();
        }


            //OPEN ADD CONTRACTS WINDOW
        public Command<int> AddContractButton
        {
            get { return new Command<int>(openWindowAddContract, (i) => { return true; }); }
        }

        private void openWindowAddContract(int estateId)
        {
            Service.NavigationService.ShowDialog<AddContractWindow, AddContractViewModel>(estateId);
        }


        //COMMANDE


    }


}

