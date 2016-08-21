using System;
using System.Collections.Generic;
using Enterprise.Model;

namespace Enterprise.NUnitTests.Data.TestFactories
{
    public class TestAuthorModelFactory
    {
        public List<AuthorModel> CrateAuthors()
        {
            List<AuthorModel> authorListing = new List<AuthorModel>();
            authorListing.Add(Author1);
            return authorListing;
        }

        public AuthorModel CreateAuthor()
        {
            return Author1;
        }

        private AuthorModel Author1
        {
            get 
            {
                AuthorModel author = new AuthorModel("Fedor", "Dostoevskii");
                author.SetAssignedIdTo(1);
                return author;
            }
        }
    }
}
