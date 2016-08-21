using System;
using System.Collections.Generic;
using Enterprise.CoreData.DataInterfaces;
using ProjectBase.Data;
using ProjectBase.Utils;
using Ninject;

namespace Enterprise.CoreData.Domain
{
    public class ApprovedOrder:Entity<long>,IHasAssignedId<long>
    {
        public ApprovedOrder() { }

        public ApprovedOrder(Order order)
        {
            Check.Require( order != null,"Order must be not null" );
            CurrentOrder = order;
            foreach (Item item in order.OrderItems)
            {
                if (item.IsOrdered == false)
                    item.IsOrdered = true;

                item.PlanedRecoveringDate = order.OrderDate.AddDays(3);
                AddApprovedItem(item);
            }
            order.OrderedByReader.AddApprovedOrder(this);
        }

        public virtual void AddApprovedItem(Item item)
        {
            Check.Require(item != null, "Item to approved by order must not be null");
            item.ApprovedOrder = this;
            OrderItems.Add(item);
        }       

        public virtual string ApprovedNumber
        {
            get { return approvedNumber; }
            set
            {
                Check.Require( !string.IsNullOrEmpty(value), "ApprovedNumber must be provided" );
                approvedNumber = value;
            }
        }

        public virtual IList<Item> OrderItems
        {
            get { return orderItems; }
            set
            {
                Check.Require( value != null, "OrderItems must not be null" );
                orderItems = value;
            }
        }

        public virtual Order CurrentOrder
        {
            get { return currentOrder; }
            protected set { currentOrder = value; }
        }

        public virtual Reader ApprovedByReader
        {
            get { return approvedByReader; }
            set { approvedByReader = value; }
        }

        public virtual DateTime OrderedDate
        {
            get { return orderedDate; }
            set { orderedDate = value; }
        }

        public virtual DateTime RecoveredDate
        {
            get { return recoveredDate; }
            set { recoveredDate = value; }
        }

        public virtual void SetAssignedIdTo(long assignedId)
        {
            ID = assignedId;
        }

        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + ApprovedNumber).GetHashCode();
        }
        
        private string approvedNumber = "";        
        private IList<Item> orderItems = new List<Item>();
        private Order currentOrder;
        protected Reader approvedByReader;
        public DateTime orderedDate;
        private DateTime recoveredDate;
    }
}
