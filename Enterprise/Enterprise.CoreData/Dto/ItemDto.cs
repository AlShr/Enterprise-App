using System;
using ProjectBase.Data;
using ProjectBase.Utils;

namespace Enterprise.CoreData.Dto
{
    [Serializable]
    public class ItemDto : BaseDto<long>
    {
        public bool IsOrdered
        {
            get { return isOrdered; }
            set { isOrdered = value; }
        }
        public bool IsReadCarted
        {
            get { return isReadCarted; }
            set { isReadCarted = value; }

        }
        public string InventotySerialCode
        {
            get { return inventorySerialCode; }
            set { inventorySerialCode = value; }
        }

        public DateTime RecoveredDate
        {
            get { return recoveredDate; }
            set { recoveredDate = value; }
        }

        public DateTime PlanedRecoveringDate
        {
            get { return planedRecoveringDate; }
            set { planedRecoveringDate = value; }
        }

        public BookDto Book
        {
            get { return book; }
            set { book = value; }
        }

        public ReaderCartSelectionDto ReaderCartSelection
        {
            get { return readerCartSelection; }
            set { readerCartSelection = value; }
        }

        private DateTime planedRecoveringDate;
        private ReaderCartSelectionDto readerCartSelection;
        private bool isOrdered = false;
        private bool isReadCarted = false;
        private DateTime recoveredDate;
        private string inventorySerialCode = "";
        private BookDto book;
    }
}
