using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;


namespace Enterprise.Model
{
    public class PageSelector:INotifyPropertyChanged
    {
        public int CurrentPage 
        {
            get { return currentPage; }
            set 
            { 
                currentPage = value;
                SetProperty(ref currentPage, value);
            }
        }

        public int PageNumber
        {
            get { return pageNumber; }
            set 
            { 
                pageNumber = value;
                SetProperty(ref pageNumber, value);
            }
        }

        public int PageCount
        {
            get { return pageCount; }
            set 
            { 
                pageCount = value;
                SetProperty(ref pageCount, value);
            }
        }

        public int PageSize
        {
            get { return pageSize; }
            set 
            {
                pageSize = value; 
                SetProperty(ref pageSize, value);               
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
        private int pageCount;
        private int pageNumber;
        private int pageSize = 200;
        private int currentPage;
    }
}
