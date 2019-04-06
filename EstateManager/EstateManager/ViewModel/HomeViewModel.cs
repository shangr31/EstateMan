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
        
        public KeyValuePair<String, double>[] Fseries
        {
            get { return GetProperty<KeyValuePair<String, double>[]>(); }
            set { SetProperty(value);  }
        }
        
        public KeyValuePair<String, double>[] Tseries
        {
            get { return GetProperty<KeyValuePair<String, double>[]>(); }
            set { SetProperty(value); }
        }

        private void LoadColumnChartData()
        {
            KeyValuePair<String, double>[] points = new KeyValuePair<String, double>[3];
            KeyValuePair<String, double>[] pts = new KeyValuePair<String, double>[6];

            points[0] = new KeyValuePair<String, double>("chien", 56);
            points[1] = new KeyValuePair<String, double>("chat", 54);
            points[2] = new KeyValuePair<String, double>("canary", 87);

            Fseries = points;

            pts[0] = new KeyValuePair<String, double>("chien", 45);
            pts[1] = new KeyValuePair<String, double>("chat", 123);
            pts[2] = new KeyValuePair<String, double>("canary", 35);
            pts[3] = new KeyValuePair<String, double>("poule", 5);
            pts[4] = new KeyValuePair<String, double>("cheveau", 5);
            pts[5] = new KeyValuePair<String, double>("vache", 7);

            Tseries = pts;
        }
    }
}
