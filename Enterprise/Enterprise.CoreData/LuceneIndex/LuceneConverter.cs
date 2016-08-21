using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lucene.Net.Documents;
using Lucene.Net.Search;
using Enterprise.CoreData.Domain;

namespace Enterprise.CoreData.LuceneIndex
{
    public static class LuceneConverter
    {
        public static Document ToDocument(BookToAuthor relation)
        {
            var document = new Document();
            document.Add(new Field("ID", relation.ID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("Book", relation.Book.Description, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Author", relation.Author.LastName, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Publisher", relation.Book.Publisher.Title, Field.Store.YES, Field.Index.ANALYZED));
            return document;
        }

        public static List<Document> ToDocumentList(IEnumerable<BookToAuthor> relations)
        {
            return relations.Select(ToDocument).ToList();
        }

        public static long ToRelationId(Document document)
        {
            Int64 id;
            return Int64.TryParse(document.GetField("ID").StringValue, out id) == true
                ? id
                : default(Int64);
        }

        public static IEnumerable<long> ToRelationIdList(IEnumerable<Document> documents)
        {
            return documents.Select(ToRelationId).ToList();
        }

        public static IEnumerable<long> ToRelationIdList(IEnumerable<ScoreDoc> hits, Searchable searcher)
        {
            return hits.Select(hit => ToRelationId(searcher.Doc(hit.Doc))).ToList();
        }

        public static string BookToString(Book book)
        {
            return book.Description + ",";
        }

        public static string AuthorToString(Author author)
        {
            return author.LastName + ",";

        }
    }
}
