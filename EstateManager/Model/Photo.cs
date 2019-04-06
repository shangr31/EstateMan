using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateManager.Model
{
    public class Photo : BaseNotifyPropertyChanged
    {

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
        public Estate estate {
            get { return GetProperty<Estate>(); }
            set { SetProperty(value); }
        }
        public String Content
        {
            get { return GetProperty<String>(); }
            set { SetProperty(value); }
        }
        public String Title
        {
            get { return GetProperty<String>(); }
            set { SetProperty(value); }
        }

        public Photo()
        { }
        public Photo(int id, int estateId, string content, string title)
        {
            Id = id;
            EstateId = estateId;
            Content = content;
            Title = title;
        }
    }
}
