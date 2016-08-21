using System;
using System.Collections.Generic;
using ProjectBase.Data;
using ProjectBase.Utils;
using NHibernate.Search.Attributes;


namespace Enterprise.CoreData.Domain
{  
    public class Book:Entity<long>,IHasAssignedId<long>
    {
        public Book() { }

        public Book(Publisher publisher)
        {
            Check.Require(publisher != null, "Book without Publisher shouldn`t exist");
            publisher.PublisherBooks.Add(this);
            Publisher = publisher;            
        }
     
        public virtual string Description
        {
            get { return description; }
            set 
            {
                Check.Require(!string.IsNullOrEmpty(value), "Description Book must be provided");
                description = value;
            }
        }

        public virtual string ISBN
        {
            get { return isbn; }
            set { isbn = value; }
        }

        public virtual double PenaltyPrice
        {
            get { return penaltyPrice; }
            set 
            {
                Check.Require(value > 0, "PenaltyPrice must be >0 ");
                penaltyPrice = value;
            }
        }

        public virtual Publisher Publisher
        {
            get { return publisher; }
            set
            {
                Check.Require(value != null, "Publisher must be not null");
                publisher = value;
            }

        }
       
        public virtual IList<ReaderCartSelection> CartSelectionWithBook
        {
            get { return cartSelectionWithBook; }
            set 
            {
                Check.Require(value != null, "CartSelectionWithThisBook must be not null");
                cartSelectionWithBook = value;
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

       
        public virtual IList<BookToAuthor> Authors 
        {
            get { return authors; }
            set { authors = value; } 
        }

        private IList<BookToAuthor> authors = new List<BookToAuthor>();    
        private IList<ReaderCartSelection> cartSelectionWithBook = new List<ReaderCartSelection>();      
        private Publisher publisher;
        private string description="";
        private string isbn = "";
        private double penaltyPrice;
        
    }
}
