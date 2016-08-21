using System;
using System.Collections.Generic;
using ProjectBase.Data;
using ProjectBase.Utils;
using Enterprise.CoreData.Domain;


namespace Enterprise.CoreData.Dto
{
    [Serializable]
    public class ReaderCartSelectionDto:BaseDto<long>
    {
        public BookDto CurrentBook
        {
            get { return currentBook; }
            set { currentBook = value; }
        }

        public ReadingCartDto ParentCart
        {
            get { return parentCart; }
            set { parentCart = value; }
        }

        private BookDto currentBook;
        private ReadingCartDto parentCart;
    }
}
