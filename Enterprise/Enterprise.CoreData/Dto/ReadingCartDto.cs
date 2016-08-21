using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Enterprise.CoreData.Dto
{
    [Serializable]
    public class ReadingCartDto:BaseDto<long>
    {
        
        public ReaderDto CartOfReader
        {
            get { return cartOfReader; }
            set { cartOfReader = value; }
        }

        public OrderDto OrderByReadingCart
        {
            get { return orderByReadingCart; }
            set { orderByReadingCart = value; }
        }

        public string CartNumber
        {
            get { return cartNumber; }
            set { cartNumber = value; }
        }

        public List<ReaderCartSelectionDto> CartSelections
        {
            get { return cartSelections; }
            set { cartSelections = value; }
        }

        private string cartNumber = "";
        private ReaderDto cartOfReader;
        private OrderDto orderByReadingCart;
        private List<ReaderCartSelectionDto> cartSelections = new List<ReaderCartSelectionDto>();
    }
}
