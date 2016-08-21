using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Enterprise.Model
{    
    public class SearchModel : INotifyPropertyChanged
    {
        public SearchModel(string searchFilter)
        {
            this.searchFilter = searchFilter;
        }

        public string SearchFilter
        {
            get { return searchFilter; }
            set 
            {                    
                SetProperty(ref searchFilter, value);
            }
        }      

        public bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
            {
                return false;
            }
            storage = value; 
            OnPropertyChanged(propertyName); 
            return true;
        }

        protected virtual void OnPropertyChanged(string propertyName) 
        { 
            PropertyChangedEventHandler handler = PropertyChanged; 
            if (handler != null)             
            handler(this, new PropertyChangedEventArgs(propertyName)); 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private string searchFilter="";
    }
}
