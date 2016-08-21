using ProjectBase.Data;
using ProjectBase.Utils;
using System;

namespace Enterprise.CoreData.Domain
{
    public class InventoryItem:Entity<long>,IHasAssignedId<long>
    {
        public InventoryItem()
        { }

        public InventoryItem(Book bookItem)
        {
            Check.Require(bookItem != null, "Book must be provided");
            this.bookItem = bookItem;
        }

        public virtual bool IsOrdered
        {
            get { return isOrdered; }
            set { isOrdered = value; }
        }

        public virtual bool IsReadCarted
        {
            get { return isReadCarted; }
            set { isReadCarted = value; }
        }

        public virtual string InventoryCode
        {
            get { return inventoryCode; }
            set { inventoryCode = value; }
        }

        public virtual Book BookItem
        {
            get { return bookItem; }
            set { bookItem = value; }
        }


        public virtual void SetAssignedIdTo(long assignedId)
        {
            ID = assignedId;
        }

        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + InventoryCode + "|" + DateTime.Now.ToString()).GetHashCode();
        }

        private Book bookItem;
        private string inventoryCode;
        private bool isOrdered;
        private bool isReadCarted;
    }
}
