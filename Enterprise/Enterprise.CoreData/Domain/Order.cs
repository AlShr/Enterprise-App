using System;
using ProjectBase.Utils;
using ProjectBase.Data;
using System.Collections.Generic;


namespace Enterprise.CoreData.Domain
{
    public class Order:Entity<long>,IHasAssignedId<long>
    {
        public Order() { }

        public virtual DateTime OrderDate
        {
            get { return orderDate; }
            set 
            {
                orderDate = value;
            }
        }

        public virtual DateTime PlanedRecoveringDate
        {
            get { return planedRecoveringDate; }
            set 
            {
                planedRecoveringDate = value;
            }

        }
        public virtual IList<Item> OrderItems
        {
            get { return orderItems; }
            set
            {
                Check.Require(value != null, "OrderedBooks must be not null");
                orderItems = value;
            }
        }

        public virtual IList<InventoryItem> OrderInventoryItems
        {
            get { return orderinventoryItems; }
            set 
            {
                Check.Require(value != null, "OrderedBooks must be provided");
                orderinventoryItems = value;
            }
        }

        public virtual string OrderNumber
        {
            get { return orderNumber; }
            set
            {
                Check.Require(!string.IsNullOrEmpty(value), "OrderNumber must be provided");
                orderNumber = value;
            }
        }

        public virtual Reader OrderedByReader
        {
            get { return orderedByReader; }
            set
            {
                Check.Require(value != null, "OrderedByReader must be provided");
                orderedByReader = value;
            }
        }       
                
        public static void MakeItemAsOrdered(Order order)
        {
            Check.Require(order != null, "Order must be provided for make item as ordered");
            foreach (Item item in order.OrderItems)
            {
                item.IsReadCarted = false;
                item.IsOrdered = true;
            }
        }

        public static void MakeInventoryItemAsOrdered(Order order)
        {
            Check.Require(order != null, "Order must be provided for make item as ordered");
            foreach (InventoryItem inventoryItem in order.OrderInventoryItems)
            {
                inventoryItem.IsReadCarted = false;
                inventoryItem.IsOrdered = true;
            }
        }

        public virtual void SetAssignedIdTo(long assignedId)
        {
            ID = assignedId;
        }

        public override int GetHashCode()
        {
            return (GetType().FullName+"|"+
                (OrderDate).GetHashCode()).GetHashCode();
        }

        private IList<InventoryItem> orderinventoryItems = new List<InventoryItem>();
        private IList<Item> orderItems = new List<Item>();
        private DateTime orderDate;
        private DateTime planedRecoveringDate;
        private string orderNumber;
        private Reader orderedByReader; 
    }
}
