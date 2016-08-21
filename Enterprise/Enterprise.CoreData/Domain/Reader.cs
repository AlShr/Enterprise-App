using System;
using System.Collections.Generic;
using ProjectBase.Data;
using ProjectBase.Utils;


namespace Enterprise.CoreData.Domain
{
    public class Reader : Entity<long>, IHasAssignedId<long>
    {
        public Reader() { }

        public Reader(Email emailIdentity)
        {
            Check.Require(emailIdentity != null, "Email must be provided");
            this.emailIdentity = emailIdentity;
        }

        public virtual Email EmailIdentity
        {
            get { return emailIdentity; }
            set
            {
                Check.Require(value != null, "Email must be not null");
                emailIdentity = value;
            }
        }

        public virtual IList<ApprovedOrder> ReaderApprovedOrders
        {
            get { return readerApprovedOrders; }
            set
            {
                Check.Require(value != null, "ReaderApprovedOrders must not be null");
                readerApprovedOrders = value;
            }
        }

        public virtual ReadingCart ReaderCart
        {
            get { return readerCart; }
            set { readerCart = value; }
        }

        public virtual void AddApprovedOrder(ApprovedOrder order)
        {
            Check.Require(order != null, "ApprovedOrder must be provided ");
            
            order.ApprovedByReader = this;
            ReaderApprovedOrders.Add(order);
        }

        public virtual void SetAssignedIdTo(long assignedId)
        {
            ID = assignedId;
        }

        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + EmailIdentity.EmailAddress).GetHashCode();
        }

        private Email emailIdentity;
        private ReadingCart readerCart;
        private IList<ApprovedOrder> readerApprovedOrders = new List<ApprovedOrder>();
    }
}
