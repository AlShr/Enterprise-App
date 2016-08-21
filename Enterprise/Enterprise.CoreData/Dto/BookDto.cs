using ProjectBase.Utils;
using System;
using System.Collections.Generic;


namespace Enterprise.CoreData.Dto
{
    [Serializable]
    public class BookDto:BaseDto<long>
    {
        
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Isbn
        {
            get { return isbn; }
            set { isbn = value; }
        }

        public double PenaltyPrice
        {
            get { return penaltyPrice; }
            set { penaltyPrice = value; }
        }

        public List<AuthorDto> BookAuthors
        {
            get { return bookAuthors; }
            set 
            {
                Check.Require(value != null, "BookAuthors must not be null");
                bookAuthors = value; 
            }
        }

        public PublisherDto Publisher
        {
            get { return publisher; }
            set 
            {
                Check.Require(value != null, "Publisher must be not null");
                publisher = value; 
            }
        }

        public long PublisherId
        {
            get { return publisherId; }
            set 
            {
                publisherId = value;
            }
        }

        private long publisherId;
        private string description;
        private string isbn;
        private double penaltyPrice;
        private PublisherDto publisher;
        private List<AuthorDto> bookAuthors = new List<AuthorDto>();
        
        
    }
}
