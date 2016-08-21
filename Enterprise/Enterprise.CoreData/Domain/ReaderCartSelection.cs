using System;
using ProjectBase.Data;
using ProjectBase.Utils;

namespace Enterprise.CoreData.Domain
{
    public class ReaderCartSelection : Entity<long>, IHasAssignedId<long>
    {
        public ReaderCartSelection() { }

        public ReaderCartSelection(Book book, ReadingCart cart, Item item)
        {
            Check.Require(book != null, "Book must be provided");
            Check.Require(cart != null, "ReadingCart must be provided");

            book.CartSelectionWithBook.Add(this);
            cart.CartSelections.Add(this);
            this.ParentCart = cart;
            this.CurrentBook = book;
            this.Item = item;
        }

        public virtual Book CurrentBook
        {
            get { return currentBook; }
            set
            {
                Check.Require(value != null, "CurrentBook must be provided");
                currentBook = value;
            }
        }

        public virtual ReadingCart ParentCart
        {
            get { return parentCart; }
            set
            {
                Check.Require(value != null, "ParentCart must be provided");
                parentCart = value;
            }
        }

        public virtual Item Item
        {
            get { return item; }
            set { item = value; }
        }

        public virtual void SetAssignedIdTo(long asignedId)
        {
            ID = asignedId;
        }

        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + CurrentBook.ID + "|" + ParentCart.ID).GetHashCode();
        }

        private Book currentBook;
        private ReadingCart parentCart;
        private Item item;
    }
}
