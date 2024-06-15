using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace testproject.ViewModels
{
     class MainViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged.Invoke(this, new  PropertyChangedEventArgs(propertyName));
        }
    }
}
