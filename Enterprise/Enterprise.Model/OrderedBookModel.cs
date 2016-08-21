using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Model
{
    public class OrderedBookModel:BaseModel<long>
    {
        public DateTime OrderDate
        {
            get { return orderdate; }
            set
            {
                orderdate = value;
                this.SetProperty(ref orderdate, value);
            }
        }

        public DateTime RecoveredDate
        {
            get { return recovereddate; }
            set 
            {
                recovereddate = value;
                this.SetProperty(ref recovereddate, value);
            }
        }

        public string OrderNumber
        {
            get { return orderNumber; }
            set
            {
                orderNumber = value;
                this.SetProperty(ref orderNumber, value);
            }
        }
        public List<BookModel> Items
        {
            get { return items; }
            set 
            {
                items = value;
                this.SetProperty(ref items, value);
            }
        }

        public virtual void SetIAssignedIdTo(long assignedId)
        {
            ID = assignedId;
        }

        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + OrderDate + "|" + OrderNumber).GetHashCode();
        }

        private string orderNumber;
        private DateTime orderdate;
        private DateTime recovereddate;
        private List<BookModel> items = new List<BookModel>();
    }
}
