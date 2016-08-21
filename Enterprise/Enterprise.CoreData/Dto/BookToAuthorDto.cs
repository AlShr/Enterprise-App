using System;
using System.Collections.Generic;
using ProjectBase.Utils;

namespace Enterprise.CoreData.Dto
{
    [Serializable]
    public class BookToAuthorDto:BaseDto<long>
    {
        public BookDto Book
        {
            get { return book; }
            set { book = value; }
        }
        public AuthorDto Author
        {
            get { return author; }
            set { author = value; }
        }

        public int PosAuthorList
        {
            get { return posAuthorList; }
            set { posAuthorList = value; }
        }

        private BookDto book;
        private AuthorDto author;
        private int posAuthorList;
    }
}
