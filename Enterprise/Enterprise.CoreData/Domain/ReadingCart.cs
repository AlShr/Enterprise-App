using System;
using ProjectBase.Utils;
using ProjectBase.Data;
using System.Collections.Generic;


namespace Enterprise.CoreData.Domain
{
    public class ReadingCart : Entity<long>, IHasAssignedId<long>
    {
        public ReadingCart() { }

        public ReadingCart(Reader cartOfReader)
        {
            Check.Require(cartOfReader != null, "Reader must be provided");
            this.cartOfReader = cartOfReader;
        }

       
        public virtual string CartNumber
        {
            get { return cartNumber; }
            set
            {
                Check.Require(!string.IsNullOrEmpty(value), "CartNumber must be provided");
                cartNumber = value;
            }
        }

        public virtual Reader CartOfReader
        {
            get { return cartOfReader; }
            set
            {
                cartOfReader = value;
            }
        }

        public virtual Order OrderByReadingCart
        {
            get { return orberByReadingCart; }
            set { orberByReadingCart = value; }
        }

        public virtual IList<ApprovedOrder> ApprovedOrderByCart
        {
            get { return approvedOrderByCart; }
            set { approvedOrderByCart = value; }
        }

        public virtual IList<ReaderCartSelection> CartSelections
        {
            get { return cartSelections; }
            set
            {
                Check.Require(value != null, "CartSelections must be provided");
                cartSelections = value;
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

        private string cartNumber = "";
        private Reader cartOfReader;
        private Order orberByReadingCart;
        private IList<ApprovedOrder> approvedOrderByCart = new List<ApprovedOrder>();
        private IList<ReaderCartSelection> cartSelections = new List<ReaderCartSelection>();
    }
}
