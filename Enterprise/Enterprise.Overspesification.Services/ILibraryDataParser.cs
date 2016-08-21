using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enterprise.Model;

namespace Enterprise.Overspesification.Services
{
    public interface ILibraryDataParser
    {
        IDictionary<BookModel, List<AuthorModel>> BookAuthorsParse(IEnumerable<BookToAuthorModel> bookToauthors);
        IDictionary<PublisherModel, List<BookModel>> PublisherBooksParse(IEnumerable<BookToAuthorModel> bookToauthors);
        List<BookModel> BookParse(
            List<BookModel> books, IDictionary<BookModel, List<AuthorModel>> bookcatalog);
        IList<PublisherModel> PublishesrParse(
            List<PublisherModel> publishers,
            IDictionary<PublisherModel, List<BookModel>> publishercatalog,
            IDictionary<BookModel, List<AuthorModel>> bookcatalog);
        List<AuthorModel> AuthorParse(IEnumerable<BookToAuthorModel> bookToauthors);
        void LibraryParser(IEnumerable<BookToAuthorModel> bookToauthors);

        List<Model.PublisherModel> PublisherModel { get; set; }
        List<Model.AuthorModel> AuthorLookUp { get; set; }
        List<Model.BookModel> BookLookUp { get; set; }
        List<Model.PublisherModel> PublisherLookUp { get; set; }
    }
}
