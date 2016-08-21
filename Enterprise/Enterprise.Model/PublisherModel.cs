using System;
using System.Collections.Generic;
using System.Linq;
using ProjectBase.Data;
using ProjectBase.Utils;

namespace Enterprise.Model
{
    public class PublisherModel : BaseModel<long>
    {
        public PublisherModel() { }

        public PublisherModel(string title)
        {
            Check.Require(!string.IsNullOrEmpty(title), "Publisher must be provided");
            this.title = title;
        }

        public string Title
        {
            get { return title; }
            set
            {
                this.SetProperty(ref title, value);
            }
        }

        public List<BookModel> Books
        {
            get { return books; }
            set
            {
                books = value;
                this.SetProperty(ref books, value);
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
        private List<BookModel> books = new List<BookModel>();
    }
}
