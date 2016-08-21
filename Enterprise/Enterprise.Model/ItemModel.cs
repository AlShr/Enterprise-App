using System;
using System.Collections.Generic;
using ProjectBase.Data;

namespace Enterprise.Model
{
    public class ItemModel : BaseModel<long>, IComparable<ItemModel>
    {
        public ItemModel()
        { }

        public bool IsOrdered
        {
            get { return isOrdered; }
            set
            {
                isOrdered = value;
                this.SetProperty(ref isOrdered, value);
            }
        }

        public bool IsReadCarted
        {
            get { return isReadCarted; }
            set
            {
                isReadCarted = value;
                this.SetProperty(ref isReadCarted, value);
            }
        }

        public string InventorySerialCode
        {
            get { return inventorySerialCode; }
            set
            {
                inventorySerialCode = value;
                this.SetProperty(ref inventorySerialCode, value);
            }
        }

        public long BookId
        {
            get { return bookId; }
            set
            {
                bookId = value;
                this.SetProperty(ref bookId, value);
            }
        }

        public BookModel Book
        {
            get { return book; }
            set
            {
                book = value;
                this.SetProperty(ref book, value);
            }
        }

        public DateTime RecoveredDate
        {
            get { return recoveredDate; }
            set 
            { 
                recoveredDate = value;
                this.SetProperty(ref recoveredDate, value);
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

        public long ApprovedOrderId
        {
            get { return approvedOrderId; }
            set { approvedOrderId = value; }
        }

        public ReaderCartSelectionModel ReaderCartSelection
        {
            get { return readerCartSelection; }
            set { readerCartSelection = value; }
        }

        public virtual void SetAssignedIdTo(long assinedId)
        {
            ID = assinedId;
        }

        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + InventorySerialCode).GetHashCode();
        }

        public int CompareTo(ItemModel other)
        {
            if (other == null)
            {
                return 1;
            }
            return ID.CompareTo(other.ID);
        }


        private DateTime planedRecoveringDate;
        private bool isOrdered = false;
        private bool isReadCarted = false;
        private DateTime recoveredDate;
        private string inventorySerialCode = "";
        private long bookId;
        private BookModel book;
        private long approvedOrderId;
        private ReaderCartSelectionModel readerCartSelection;
    }
}
