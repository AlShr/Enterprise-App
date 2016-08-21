using System;
using System.Collections.Generic;
using ProjectBase.Data;

namespace Enterprise.Model
{
    public class InventoryItemModel:BaseModel<long>,IComparable<InventoryItemModel>
    {
        public InventoryItemModel()
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
                IsReadCarted = value;
                this.SetProperty(ref isReadCarted, value);
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

        public virtual void SetAssignedIdTo(long assignedId)
        {
            ID = assignedId;
        }

        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + DateTime.Now).GetHashCode();
        }

        public int CompareTo(InventoryItemModel other)
        {
            if (other == null)
            {
                return 1;
            }
            return ID.CompareTo(other.ID);
        }

        private BookModel book;
        private long bookId;
        private bool isOrdered = false;
        private bool isReadCarted = false;

    }
}
