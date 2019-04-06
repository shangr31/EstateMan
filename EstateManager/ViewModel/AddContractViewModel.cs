using EstateManager.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EstateManager.ViewModel
{
    class AddContractViewModel : WindowNotifyPropertyChanged
    {
        private int estateId;

        public int ContractType {
            get { return GetProperty<int>(); }
            set { SetProperty(value); }
        }

        public string DescriptionT {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        public string TitleT {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        public DateTime PubDateT {
            get { return GetProperty<DateTime>(); }
            set { SetProperty(value); }
        }

        public DateTime? SignDateT {
            get { return GetProperty<DateTime?>(); }
            set { SetProperty(value); }
        }

        public DateTime? CloseDateT {
            get { return GetProperty<DateTime?>(); }
            set { SetProperty(value); }
        }

        public string PriceT {
            get { return GetProperty<string>(); }
            set {
                SetProperty(value);
            }
        }


        public AddContractViewModel(Window v, int estateId) : base(v)
        {
            this.estateId = estateId;
            PubDateT = DateTime.Today;
            SignDateT = DateTime.Today;
            CloseDateT = DateTime.Today;
            PriceT = "0,00";
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
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

        public Command validateButton
        {
            get
            {
                return new Command(validate, () => { return true; });

            }
        }

        public void validate()
        {
            if (!canBeValidate()) return;
            var c = new Contract()
            {
                
                EstateId = estateId,
                Description = DescriptionT,
                Title = TitleT,
                PubDate = PubDateT,
                SignDate = SignDateT,
                CloseDate = CloseDateT,
                Price = Decimal.Parse(PriceT)
            };
            c.Type = (Contract.ContractType)ContractType + 1;


            DataAccess.EstateDbContext.Current.Contracts.Add(c);
            DataAccess.EstateDbContext.Current.SaveChanges();
        }

        public bool canBeValidate()
        {
            if (DescriptionT != null && TitleT != null && PubDateT != null)
            {
                if (!isDate(PubDateT)) return false;
                if (!isDate(SignDateT) && SignDateT != null) return false;
                if (!isDate(CloseDateT) && CloseDateT != null) return false;
                if (!isPrice(PriceT.ToString())) return false;
                return true;
            }
            return false;
        }

        private bool isDate(DateTime? date)
        {
            if (date == null) return true;

            DateTime d = (DateTime)date;

            if (DateTime.TryParseExact(date.ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out d)) return true;

            return false;
        }

        private bool isPrice(string price)
        {
            if (price == null) return false;
            if (Decimal.TryParse(price, out decimal decPrice)) return true;
            return false;
        }

    }
}
