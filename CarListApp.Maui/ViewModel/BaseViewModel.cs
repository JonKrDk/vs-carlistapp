using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.Maui.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private string _title;
        private bool   _isBusy;

        public string Title
        {
            get => _title;

            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsBusy 
        { 
            get => _isBusy;

            set
            {
                if (_isBusy != value)
                { 
                    _isBusy = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
