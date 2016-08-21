using System;
using System.Collections.Generic;
using ProjectBase.Data;
using ProjectBase.Utils;

namespace Enterprise.Model
{
    public class BookModel : BaseModel<long>, IComparable<BookModel>
    {
        public BookModel() { }

        public BookModel(PublisherModel publisher)
        {
            Check.Require(publisher != null, "Book withot Publisher shouldn`t exist");
            publisher.Books.Add(this);
            Publisher = publisher;
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                this.SetProperty(ref description, value);
            }
        }

        public string ISBN
        {
            get { return isbn; }
            set
            {
                isbn = value;
                this.SetProperty(ref isbn, value);
            }
        }

        

        public double PenaltyPrice
        {
            get { return penaltyprice; }
            set
            {
                penaltyprice = value;
                this.SetProperty(ref penaltyprice, value);
            }
        }

        public List<AuthorModel> Authors
        {
            get { return authors; }
            set
            {
                authors = value;
                this.SetProperty(ref authors, value);
            }
        }

        public PublisherModel Publisher
        {
            get { return publisher; }
            set
            {
                publisher = value;
                this.SetProperty(ref publisher, value);
            }
        }

        public bool IsCheked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                this.SetProperty(ref isChecked, value);
            }
        }

        public long PublisherId
        {
            get { return publisherId; }
            set 
            {
                publisherId = value;
                this.SetProperty(ref publisherId, value);
            }
        }

        public bool DetailCheck
        {
            get { return detailCheck; }
            set 
            {
                detailCheck = value;
                this.SetProperty(ref detailCheck, value);
            }
        }

        public DateTime RecoveredDate
        {
            get { return recoveeredDate; }
            set 
            {
                recoveeredDate = value;
                this.SetProperty(ref recoveeredDate, value);
            }
        }

        public DateTime PlanedRecoveringDate
        {
            get { return planedRecoveringDate; }
            set 
            {
                planedRecoveringDate = value;
                this.SetProperty(ref planedRecoveringDate, value);
            }
        }

        public virtual void SetAssignedIdTo(long assignedId)
        {
            ID = assignedId;
        }

        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + Description + "|" + ISBN).GetHashCode();
        }

        public int CompareTo(BookModel other)
        {
            if (other == null)
            {
                return 1;
            }
            return ID.CompareTo(other.ID);
        }

        private DateTime planedRecoveringDate;
        private bool detailCheck = false;
        private long publisherId;
        private DateTime recoveeredDate;
        private bool isChecked = false;
        private PublisherModel publisher;
        private double penaltyprice;
        private string description = "";
        private string isbn = "";
        private List<AuthorModel> authors = new List<AuthorModel>();
    }
}
