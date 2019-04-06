using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EstateManager.Model
{
    public class WindowNotifyPropertyChanged : BaseNotifyPropertyChanged
    {
        protected internal Window view;

        public WindowNotifyPropertyChanged(Window v) : base()
        {
            view = v;
        }
    }
}
