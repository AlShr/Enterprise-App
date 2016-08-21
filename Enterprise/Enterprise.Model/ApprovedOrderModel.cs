using System;
using System.Collections.Generic;
using ProjectBase.Data;

namespace Enterprise.Model
{
    public class ApprovedOrderModel : BaseModel<long>, IComparable<ApprovedOrderModel>
    {
        public string ApprovedNumber
        {
            get { return approvedNumber; }
            set 
            {
                approvedNumber = value;
            }
        }

        public DateTime OrderedDate
        {
            get { return orderedDate; }
            set 
            {
                orderedDate = value;
            }
        }

        public DateTime RecoveredDate
        {
            get { return recoveredDate; }
            set { recoveredDate = value; }
        }

        public IList<ItemModel> OrderItems
        {
            get { return orderItems; }
            set
            {
                orderItems = value;
            }
        }

        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + ApprovedNumber).GetHashCode();
        }

        public virtual void SetAssignedIdTo(long assignedId)
        {
            ID = assignedId;
        }

        public int CompareTo(ApprovedOrderModel other)
        {
            if (other == null)
            {
                return 1;
            }
            return ID.CompareTo(other.ID);
        }

        private IList<ItemModel> orderItems = new List<ItemModel>();
        private DateTime orderedDate;
        private DateTime recoveredDate;
        private string approvedNumber = "";
    }
}
