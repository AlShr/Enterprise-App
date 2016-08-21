using System;
using System.Collections.Generic;
using Enterprise.CoreData.Domain;


namespace Enterprise.NUnitTests.Data.TestFactories
{
    public class TestAuthorsFactory
    {
        public List<Author> CreateAuthors()
        {
            List<Author> authorListing = new List<Author>();
            authorListing.Add(Author1);
            authorListing.Add(Author2);
            return authorListing;
        }

        public Author CrateAuthor()
        {
            return Author1;
        }

        private Author Author1
        {
            get 
            {
                Author author = new Author ("Fedor","Dostoevskii");
                author.SetAssignedIdTo(1);
                return author;
            }
        }

        private Author Author2
        {
            get 
            {
                Author author = new Author("Mihail", "Bulgakov");
                author.SetAssignedIdTo(2);
                return author;
            }
        }
    }
}
