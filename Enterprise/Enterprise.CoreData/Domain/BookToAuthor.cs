using System;
using System.Collections.Generic;
using ProjectBase.Data;
using ProjectBase.Utils;
using NHibernate.Search.Attributes;
using Enterprise.CoreData.DataInterfaces;


namespace Enterprise.CoreData.Domain
{
    public class BookToAuthor : Entity<long>, IHasAssignedId<long>
    {
        public virtual Book Book
        {
            get { return book; }
            set { book = value; }
        }

        public virtual Author Author
        {
            get { return author; }
            set { author = value; }
        }

        public virtual int PosAuthorList
        {
            get { return posAuthorList; }
            set { posAuthorList = value; }
        }

        public virtual void SetAssignedIdTo(long assignedId)
        {
            ID = assignedId;
        }

        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + Book.ISBN + "|" + Author.FirstName + "|" + Author.LastName).GetHashCode();

        }

        private Book book;
        private Author author;
        private int posAuthorList;
    }
}
