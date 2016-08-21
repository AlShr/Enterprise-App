using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Enterprise.Model
{
    public class SearchLookUpModel:INotifyPropertyChanged
    {
      
        public string BookFilter
        {
            get { return bookfilter; }
            set 
            { 
                bookfilter = value;
                SetProperty(ref bookfilter, value);
            }

        }
      
        public string AuthorFilter
        {
            get { return authorfilter; }
            set 
            { 
                authorfilter = value;
                SetProperty(ref authorfilter, value);
            }
        }

        public string PublisherFilter
        {
            get { return publisherfilter; }
            set 
            { 
                publisherfilter = value;
                SetProperty(ref publisherfilter, value);
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
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private string bookfilter = "";
        private string authorfilter = "";
        private string publisherfilter = "";
    }
}
