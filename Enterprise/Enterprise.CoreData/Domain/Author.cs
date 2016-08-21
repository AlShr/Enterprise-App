using System;
using System.Collections.Generic;
using ProjectBase.Data;
using ProjectBase.Utils;


namespace Enterprise.CoreData.Domain
{
    
    public class Author:Entity<long>,IHasAssignedId<long>
    {
        public  Author() { }

        public Author(string firstName, string lastName)
        {
            Check.Require(!string.IsNullOrEmpty(firstName), "Author firstName must be provided");
            Check.Require(!string.IsNullOrEmpty(lastName), "Author lastName must be provided");
            this.firstName = firstName;
            this.lastName = lastName;
        }
       
      
        public virtual string FirstName
        {
            get { return firstName; }           
            set 
            {
                Check.Require(!string.IsNullOrEmpty(value), "Author firstname must be provided");
                firstName = value;
            }
        }

        public virtual string LastName
        {
            get { return lastName; }
            set
            {
                Check.Require(!string.IsNullOrEmpty(value), "Author lastname must be provided");
                lastName = value;

            }
        }

        public virtual IList<BookToAuthor> Books
        {
            get { return books; }
            set { books = value; }
        }

        public virtual void SetAssignedIdTo(long assignedId)
        {
            ID = assignedId;
        }

        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + FirstName + "|" + LastName).GetHashCode();

        }
       

        private IList<BookToAuthor> books = new List<BookToAuthor>();
        private string firstName = "";
        private string lastName = "";
    }
}
