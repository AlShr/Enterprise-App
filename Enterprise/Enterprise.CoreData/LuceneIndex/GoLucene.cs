using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;

using Version = Lucene.Net.Util.Version;
using Enterprise.CoreData.Domain;

namespace Enterprise.CoreData.LuceneIndex
{
    public static class GoLucene
    {
        //properties
        public static string luceneDir =
            Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "luceneindex");
        private static FSDirectory directoryTemp;
        private static FSDirectory directory 
        {
            get 
            {
                if (directoryTemp == null) directoryTemp = FSDirectory.Open(new DirectoryInfo(luceneDir));
                if (IndexWriter.IsLocked(directoryTemp)) IndexWriter.Unlock(directoryTemp);
                var lockFilePath = Path.Combine(luceneDir, "write.lock");
                if (File.Exists(lockFilePath)) File.Delete(lockFilePath);
                return directoryTemp;
            }
        }

        ////search methods
        //public static IEnumerable<BookToAuthor> GetAllIndexRecords()
        //{
        //    //validate search index
        //    if (!System.IO.Directory.EnumerateFiles(luceneDir).Any()) return new List<BookToAuthor>();

        //    //set up lucene searcher
        //    var searcher = new IndexSearcher(directory, false);
        //    var reader = IndexReader.Open(directory, false);
        //    var docs = new List<Document>();
        //    var term = reader.TermDocs();

        //    //v 2.9.4: use 'hit.Doc()'
        //    //v 3.0.3: use 'hit.Doc'

        //    while (term.Next()) docs.Add(searcher.Doc(term.Doc));
        //    reader.Dispose();
        //    searcher.Dispose();
        //    return mapLuceneToDataList(docs);
        //}


        //private static BookToAuthor mapLuceneDocumentToData(Document doc)
        //{
        //    return new BookToAuthor
        //    {
        //        ID =Convert.ToInt64(doc.Get("ID")),
        //        Book = doc.Get("BookToString"),
        //    }
        //}

        public static string BookToString(Book book)
        {
            return book.Description + ",";
        }
        private static void addToLuceneIndex(BookToAuthor simpleData, IndexWriter writer)
        {
            //remove older index entry
            var searchQuery = new TermQuery(new Term("ID", simpleData.ID.ToString()));
            writer.DeleteDocuments(searchQuery);

            //add new index entry
            var doc = new Document();

            //add lucene fields mapped to db fields
            doc.Add(new Field("ID", simpleData.ID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("Book", simpleData.Book.Description, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Author", simpleData.Author.LastName, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("Publisher", simpleData.Book.Publisher.Title, Field.Store.YES, Field.Index.ANALYZED));

            //add entry to index
            writer.AddDocument(doc);
        }
    }
}
