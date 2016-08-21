using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBase.Data;
using ProjectBase.Utils;

namespace Enterprise.CoreData.Domain
{
    public class Publisher:Entity<long>
    {
        public Publisher() { }

        public Publisher(string title)
        {
            Check.Require(!string.IsNullOrEmpty(title), "Publisher Title must be provided");
            this.title = title;
        }

        public virtual string Title
        {
            get { return title; }
            set
            {
                Check.Require(!string.IsNullOrEmpty(value), "Publisher Title must be provided");
                title = value;
            }
        }
        public virtual IList<Book> PublisherBooks
        {
            get { return publisherBooks; }
            set
            {
                Check.Require(value != null, "PublisherBooks must not be null");
                publisherBooks = value;
            }
        }

        public virtual void SetAssignedIdTo(long assignedId)
        {
            ID = assignedId;
        }

        public override int GetHashCode()
        {
            return (GetType().FullName + "Title" + "").GetHashCode();
        }

       
        private string title = "";
        private IList<Book> publisherBooks = new List<Book>();
    }
}
