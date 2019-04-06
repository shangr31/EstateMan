using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateManager.Model
{
    public class Contract : BaseNotifyPropertyChanged
    {
        public enum ContractType {Rental = 1, Sale = 2};

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get { return GetProperty<int>(); }
            set { SetProperty(value); }
        }
        
        public int EstateId
        {
            get { return GetProperty<int>(); }
            set { SetProperty(value); }
        }
        [ForeignKey(nameof(EstateId))]
        public Estate estate { get; set; }

        public ContractType Type
        {
            get { return GetProperty<ContractType>(); }
            set { SetProperty(value); }
        }
        public DateTime? SignDate
        {
            get { return GetProperty<DateTime?>(); }
            set { SetProperty(value); }
        }
        public DateTime? CloseDate
        {
            get { return GetProperty<DateTime?>(); }
            set { SetProperty(value); }
        }
        public DateTime PubDate
        {
            get { return GetProperty<DateTime>(); }
            set { SetProperty(value); }
        }
        public String Description
        {
            get { return GetProperty<String>(); }
            set { SetProperty(value); }
        }
        public String Title
        {
            get { return GetProperty<String>(); }
            set { SetProperty(value); }
        }
        public decimal Price
        {
            get { return GetProperty<decimal>(); }
            set { SetProperty(value); }
        }

        public Contract(int id, int estateId, ContractType type, DateTime signDate, DateTime closeDate, DateTime pubDate, string description, string title, decimal price)
        {
            Id = id;
            EstateId = estateId;
            Type = type;
            SignDate = signDate;
            CloseDate = closeDate;
            PubDate = pubDate;
            Description = description;
            Title = title;
            Price = price;
        }
        public Contract()
        {

        }
    }
}
