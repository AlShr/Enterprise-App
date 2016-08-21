using System;
using System.Collections.Generic;

namespace Enterprise.CoreData.Dto
{
    [Serializable]
    public class OrderDto:BaseDto<long>
    {
        public List<ItemDto> OrderItems
        {
            get { return orderItems; }
            set { orderItems = value; }
        }

        public DateTime OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; }
        }

        public string OrderNumber
        {
            get { return orderNumber; }
            set { orderNumber = value; }
        }

        private List<ItemDto> orderItems = new List<ItemDto>();
        private DateTime orderDate;
        private string orderNumber;
 
    }
}
