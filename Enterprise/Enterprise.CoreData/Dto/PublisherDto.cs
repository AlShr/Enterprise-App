using System;
using ProjectBase.Data;
using ProjectBase.Utils;
using System.Collections.Generic;

namespace Enterprise.CoreData.Dto
{
    [Serializable]
    public class PublisherDto:BaseDto<long>
    {
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public List<BookDto> Book
        {
            get { return book; }
            set { book = value; }
        }

        private List<BookDto> book = new List<BookDto>();
        private string title = "";
    }
}
