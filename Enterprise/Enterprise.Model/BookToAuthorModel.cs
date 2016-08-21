using System;
using System.Collections.Generic;
using ProjectBase.Data;


namespace Enterprise.Model
{
    public class BookToAuthorModel : BaseModel<long>
    {
        public BookModel Book
        {
            get { return book; }
            set
            {
                book = value;
                this.SetProperty(ref book, value);
            }
        }
        public AuthorModel Author
        {
            get { return author; }
            set
            {
                author = value;
                this.SetProperty(ref author, value);
            }
        }

        public int PosAuthorList
        {
            get { return posAuthorList; }
            set 
            {
                posAuthorList = value;
                this.SetProperty(ref posAuthorList, value);
            }
        }
     
        public virtual void SetAssignedIdTo(long assignedId)
        {
            ID = assignedId;
        }

        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + Book.ISBN + "|" + Author.FirstName + "|" + Author.LastName).GetHashCode();

        }

        private BookModel book;
        private AuthorModel author;
        private int posAuthorList;
    }
}
