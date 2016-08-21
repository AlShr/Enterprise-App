using ProjectBase.Data;
using ProjectBase.Utils;
using System;


namespace Enterprise.CoreData.Domain
{
    public class Item : Entity<long>, IHasAssignedId<long>
    {
        public Item()
        { }

        public Item(InventoryItem inventoryItem)
        {
            this.InventorySerialCode = inventoryItem.InventoryCode;
            this.ItemDescription = inventoryItem.BookItem;
            this.isOrdered = inventoryItem.IsOrdered;
            this.IsReadCarted = inventoryItem.IsReadCarted;
        }

        public Item(Book itemDescription)
        {
            Check.Require(itemDescription != null, "Book must be provided");
            this.itemDescription = itemDescription;
        }

        public virtual bool IsOrdered
        {
            get { return isOrdered; }
            set
            {
                isOrdered = value;
            }
        }

        public virtual bool IsReadCarted
        {
            get { return isReadCarted; }
            set
            {
                isReadCarted = value;
            }
        }

        public virtual string InventorySerialCode
        {
            get { return inventorySerialCode; }
            set
            {
                Check.Require(!string.IsNullOrEmpty(value), "SerialInventoryCode must be provided");
                inventorySerialCode = value;
            }
        }

        public virtual Order Order
        {
            get { return order; }
            set { order = value; }
        }

        public virtual ApprovedOrder ApprovedOrder
        {
            get { return approvedOrder; }
            set { approvedOrder = value; }
        }


        public virtual Book ItemDescription
        {
            get { return itemDescription; }
            set { itemDescription = value; }
        }

        public virtual DateTime RecoveredDate
        {
            get { return recoveredDate; }
            set { recoveredDate = value; }
        }

        public virtual DateTime PlanedRecoveringDate
        {
            get { return planedRecoveringDate; }
            set { planedRecoveringDate = value; }
        }
        public virtual ReaderCartSelection ReaderCartSelection
        {
            get { return readerCartSelection; }
            set { readerCartSelection = value; }
        }

        public virtual void SetAssignedIdTo(long assignedId)
        {
            ID = assignedId;
        }

        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + InventorySerialCode).GetHashCode();
        }

        private ReaderCartSelection readerCartSelection;
        private bool isOrdered = false;
        private bool isReadCarted = false;
        private DateTime recoveredDate;
        private DateTime planedRecoveringDate;
        private string inventorySerialCode = "";
        private ApprovedOrder approvedOrder;
        private Book itemDescription;
        private Order order;
    }
}
