using System;
using System.Collections.Generic;
using ProjectBase.Utils;
using ProjectBase.Data;


namespace Enterprise.Model
{
    public class ReaderCartModel : BaseModel<long>, IComparable<ReaderCartModel>
    {
        public ReaderCartModel(ReaderModel reader)
        {
            Check.Require(reader != null, "Reader must be provided");
            this.reader = reader;
        }

        public ReaderModel Reader
        {
            get { return reader; }
            set 
            {
                reader = value;
                this.SetProperty(ref reader, value);
            }
        }

        public string CartNumber 
        {
            get { return cartNumber; }
            set 
            {
                cartNumber = value;
                this.SetProperty(ref cartNumber, value);
            }
        }

        public IList<BookModel> Books 
        {
            get { return books; }
            set 
            {
                books = value;
                this.SetProperty(ref books, value);
            }
        }

        public IList<ReaderCartSelectionModel> CartSelections
        {
            get { return cartSelections; }
            set 
            {
                Check.Require(value != null, "CartSelections must be provided");
                cartSelections = value;
                this.SetProperty(ref cartSelections, value);
            }
        }
        public virtual void SetAssignedIdTo(long assignedId)
        {
            ID = assignedId;
        }

        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + CartNumber).GetHashCode();
        }

        public int CompareTo(ReaderCartModel other)
        {
            if (other == null)
            {
                return 1;
            }
            return ID.CompareTo(other.ID);
        }

        private IList<BookModel> books = new List<BookModel>();
        private IList<ReaderCartSelectionModel> cartSelections = new List<ReaderCartSelectionModel>();
        private ReaderModel reader;
        private string cartNumber = "";
    }
}
