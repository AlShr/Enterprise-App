using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enterprise.Web.DbProvider
{
    public sealed class DbContext
    {
        public DbContext()
        {
            try 
            {
                this.connStr = ConfigurationManager.ConnectionStrings["orclXEConn"].ConnectionString;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DbContext(string connStr)
        {
            this.connStr = connStr;
        }

        public DataSet AvaliableBooks()
        {
            

            string query = @"select *
                        from 
                        ( select rownum rnum, a.*
                            from (select * from Books_To_Authors Books_To_Authors
                            inner join Books Books on Books.id = Books_To_Authors.book_id
                            inner join Authors Authors on Authors.id = Books_To_Authors.author_id
                            inner join Publishers Publishers on Publishers.id = Books.publisher_id
                            inner join InventoryItems InventoryItems on InventoryItems.book_id = Books.id
                            where InventoryItems.is_ordered = 0) a
                         where rownum <= 1000)
                        where rnum >= 1";
            DataSet dataSet;
            DataTable dataTable;
            try 
            {
                using (OracleConnection conn = new OracleConnection(connStr))
                {
                    using (OracleCommand comm = new OracleCommand(query, conn))
                    {
                        dataTable = new DataTable("Books_To_Authors");
                        DataTable dtAuthors = new DataTable("Authors");
                        DataTable dtBooks = new DataTable("Books");                       
                        DataTable dtPublishers = new DataTable("Publishers");
                        OracleDataAdapter adapter = new OracleDataAdapter(comm);
                        adapter.SelectCommand = comm;
                        adapter.Fill(dataTable);

                        comm.CommandText = @"select * 
                                                from 
                                            ( select rownum rnum, a.*
                                                    from (Books) a
                                                 where rownum <= 1000)
                                            where rnum >= 1";
                        adapter.SelectCommand = comm;
                        adapter.Fill(dtBooks);

                        comm.CommandText = @"select * 
                                                from 
                                            ( select rownum rnum, a.*
                                                    from (Authors) a
                                                 where rownum <= 1000)
                                            where rnum >= 1";
                        adapter.SelectCommand = comm;
                        adapter.Fill(dtAuthors);

                        comm.CommandText = @"select * 
                                                from 
                                            ( select rownum rnum, a.*
                                                    from (Publishers) a
                                                 where rownum <= 1000)
                                            where rnum >= 1"; 
                        adapter.SelectCommand = comm;
                        adapter.Fill(dtPublishers);

                        dataSet = new DataSet("Books_To_Authors");
                        dataSet.Tables.AddRange(new DataTable[] { dataTable, dtBooks, dtAuthors, dtPublishers});
                        dataSet.Relations.Add("Books_To_AuthorsBooks", 
                          dataSet.Tables["Books"].Columns["id"],
                          dataSet.Tables["Books_To_Authors"].Columns["book_id"]);
                        dataSet.Relations.Add("Books_To_AuthorsAuthors", 
                            dataSet.Tables["Authors"].Columns["id"],
                            dataSet.Tables["Books_To_Authors"].Columns["author_id"]);
                        dataSet.Relations.Add("BooksPublishers", 
                           dataSet.Tables["Publishers"].Columns["id"],
                           dataSet.Tables["Books"].Columns["publisher_id"]);

                    }
                }
                return dataSet;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public DataSet ApprovedReportNotRecovered()
        {
            string query = @"select * 
                                from
                                ( select rownum rnum, a.*
                                    from (select * from  Approvedorders Approvedorders                            
                                    inner join Items Items ON Approvedorders.id = Items.approvedorder_id
                                    inner join Reader Reader ON Approvedorders.reader_id = Reader.reader_id
                                    inner join Books Books ON Items.book_Id = Books.id 
                                    where Items.RECOVEREDDATE < TO_DATE('0001/01/01  12.00.00', 'yyyy/mm/dd HH:MI:SS')
                                    AND Items.PLANEDRECOVERINGDATE <  CURRENT_DATE) a                           
                                 where rownum <= 1000)
                                where rnum >= 1";
            DataSet dataSet;
            DataTable dataTable;
            try 
            {
                using (OracleConnection conn = new OracleConnection(connStr))
                {
                    using (OracleCommand comm = new OracleCommand(query, conn))
                    {
                        
                        dataTable = new DataTable("Approvedorders");
                        DataTable dtItems = new DataTable("Items");
                        DataTable dtBooks = new DataTable("Books");
                        DataTable dtReaders = new DataTable("Reader");
                        
                        OracleDataAdapter adapter = new OracleDataAdapter(comm);
                        adapter.SelectCommand = comm;
                        adapter.Fill(dataTable);

                        comm.CommandText = @"select * 
                                                from 
                                                ( select rownum rnum, a.*
                                                    from (Items) a 
                                                    where rownum <= 1000)
                                                where rnum >= 1";
                        adapter.SelectCommand = comm;
                        adapter.Fill(dtItems);

                        comm.CommandText = @"select * 
                                                from 
                                            ( select rownum rnum, a.*
                                                    from (Books) a
                                                 where rownum <= 1000)
                                            where rnum >= 1";
                        adapter.SelectCommand = comm;
                        adapter.Fill(dtBooks);
                        comm.CommandText = @"select * 
                                                from 
                                            ( select rownum rnum, a.*
                                                    from (Reader) a
                                                 where rownum <= 1000)
                                            where rnum >= 1";
                        adapter.SelectCommand = comm;
                        adapter.Fill(dtReaders);

                        dataSet = new DataSet("ApproveOrders");
                        dataSet.Tables.AddRange(new DataTable[] { dataTable, dtItems, dtBooks, dtReaders });
                        dataSet.Relations.Add("FK_Approved_item", dataSet.Tables["Approvedorders"].Columns["id"],
                            dataSet.Tables["Items"].Columns["approvedorder_id"], false);
                        dataSet.Relations.Add("ApprovedordersReader", dataSet.Tables["Approvedorders"].Columns["reader_id"],
                            dataSet.Tables["Reader"].Columns["reader_id"], false);

                    }
                }
                return dataSet;
            }
            catch (Exception ex)
            {
                throw;
            }           
        }

        private readonly string connStr;
    }
}