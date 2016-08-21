using System;
using System.Collections.Generic;
using ProjectBase.Data;

namespace Enterprise.Model
{
    public class ReaderCartSelectionModel : BaseModel<long>
    {
        public ReaderCartSelectionModel()
        {
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

        public ReaderCartModel ParentCart
        {
            get { return parentcart; }
            set
            {
                parentcart = value;

            }
        }

        public void SetAssignedIdTo(long assignedId)
        {
            ID = assignedId;
        }

        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + Book.ID + "|" + ParentCart.ID).GetHashCode();
        }

        private ReaderCartModel parentcart;
        private BookModel book;
    }
}
