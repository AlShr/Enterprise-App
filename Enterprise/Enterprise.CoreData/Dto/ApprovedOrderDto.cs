using System;
using System.Collections.Generic;
using ProjectBase.Data;
using ProjectBase.Utils;


namespace Enterprise.CoreData.Dto
{
    [Serializable]
    public class ApprovedOrderDto : BaseDto<long>
    {
        public string ApprovedNumber
        {
            get { return approvedNumber; }
            set
            {
                approvedNumber = value;
            }
        }

        public List<ItemDto> OrderItems
        {
            get { return orderItems; }
            set
            {
                orderItems = value;
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

        private DateTime recoveredDate;
        private DateTime orderedDate;
        private List<ItemDto> orderItems = new List<ItemDto>();
        private string approvedNumber = "";
    }
}
