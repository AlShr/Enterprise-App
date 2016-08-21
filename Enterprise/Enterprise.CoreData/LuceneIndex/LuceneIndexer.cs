using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Directory = Lucene.Net.Store.Directory;
using Version = Lucene.Net.Util.Version;
using Enterprise.CoreData.Domain;
using ProjectBase.Utils;
using System.Web;

namespace Enterprise.CoreData.LuceneIndex
{
    public class LuceneIndexer
    {
       
        public static LuceneIndexer Instance
        {
            get 
            {
                return Nested.LuseneIndexer;
            }
        }


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
        //private LuceneIndexer()
        //{
        //    var directory = Environment.ExpandEnvironmentVariables("%USERPROFILE%\\Lucene.dir");
        //    if (System.IO.Directory.Exists(directory))
        //    {
        //        System.IO.Directory.CreateDirectory(directory);
        //    }
        //    indexDirectory = new SimpleFSDirectory(new DirectoryInfo(directory));
        //}

        private class Nested
        {
            internal static readonly LuceneIndexer LuseneIndexer = new LuceneIndexer();
            static Nested() { }
        }

        public void Add(BookToAuthor relation)
        {
            using (var writer = new IndexWriter(directory, new StandardAnalyzer(Version.LUCENE_30), IndexWriter.MaxFieldLength.UNLIMITED))
            {
                var searchQuery = new TermQuery(new Term("Id", relation.ID.ToString()));
                writer.AddDocument(LuceneConverter.ToDocument(relation));
            }
        }

        public void AddRange(IEnumerable<BookToAuthor> relations)
        {
            foreach (var relation in relations)
            {
                Add(relation);
            }
        }

        private Query ParseQuery(string searchQuery, QueryParser parser)
        {
            Query query;
            try
            {
                query = parser.Parse(searchQuery.Trim());
            }
            catch (ParseException)
            {
                query = parser.Parse(QueryParser.Escape(searchQuery.Trim()));
                throw;
            }
            return query;
        }

        private IEnumerable<long> SearchRelations(string searchQuery, string[] fields = null)
        {
            if (((fields == null) || (fields.Length == 0)) ||
                string.IsNullOrEmpty(searchQuery.Replace("*", "").Replace("?", "")))
            {
                throw new ApplicationException("Cant search in noone field");
            }

            using (var searcher = new IndexSearcher(directory, false))
            {
                var hitsLimit = 1000;
                var analyzer = new StandardAnalyzer(Version.LUCENE_30);
                var parser = new MultiFieldQueryParser(Version.LUCENE_30, fields, analyzer);
                var query = ParseQuery(searchQuery, parser);
                var hits = searcher.Search(query, null, hitsLimit, Sort.RELEVANCE);
                var score = hits.ScoreDocs;
                var results = LuceneConverter.ToRelationIdList(score, searcher);
                analyzer.Close();
                searcher.Dispose();
                return results;
            }
        }

        public IEnumerable<long> Search(string input, string[] fields = null)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new List<long>();
            }

            var terms = input.Trim().Replace("-", "").Split(' ')
                .Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim());
            input = string.Join(" ", terms);

            return SearchRelations(input, fields);
        }

        public IEnumerable<long> GetAllRecords()
        {
            if (directory.ListAll().Length == 0)
            {
                return new List<long>();
            }

            var seacher = new IndexSearcher(directory, false);
            var reader = IndexReader.Open(directory, false);
            var doc = new List<Document>();
            var term = reader.TermDocs();
            while (term.Next())
            {
                doc.Add(seacher.Doc(term.Doc));
            }
            reader.Dispose();
            seacher.Dispose();
            return LuceneConverter.ToRelationIdList(doc);
        }

        public void ClearLuceneIndexRecord(long recordId)
        {
            //init lucene
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            using (var writer = new IndexWriter(directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                //remove older index entry
                var searchQuery = new TermQuery(new Term("ID", recordId.ToString()));
                writer.DeleteDocuments(searchQuery);
                analyzer.Close();
                writer.Dispose();
            }
 
        }

        public bool ClearLuceneIndex()
        {
            try
            {
                var analyzer = new StandardAnalyzer(Version.LUCENE_30);
                using (var writer = new IndexWriter(directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    //remove older index entries
                    writer.DeleteAll();

                    analyzer.Close();
                    writer.Dispose();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }      
        //private readonly Directory indexDirectory;
    }
}
