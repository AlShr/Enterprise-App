using Enterprise.CoreData.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.CoreData.SampleDataRepository
{
    public static class SampleDataRepository
    {
        public static BookToAuthor Get(int id)
        {
            return GetAll().SingleOrDefault(x => x.ID.Equals(id));
        }

        public static List<BookToAuthor> GetAll()
        {
            return new List<BookToAuthor> {
                new BookToAuthor {
                ID = 1, 
                Author = new Author { ID = 1, FirstName="Fedor",LastName = "Dostoevskii`"},
                Book = new Book { ID = 1, Description = "The Stranger by Albert Camus" }},
                new BookToAuthor {
                ID = 2, 
                Author = new Author { ID = 2, FirstName="Lev",LastName = "Tolstoi``"},
                Book = new Book { ID = 2, Description = "One Hundred Years of Solitude by Gabriel Marquez" }},
            };
        }
    }
}
