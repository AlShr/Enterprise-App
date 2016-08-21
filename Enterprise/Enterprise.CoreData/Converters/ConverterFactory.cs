using Enterprise.CoreData.ConverterFactory;
using Enterprise.CoreData.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using ProjectBase.Data;
using Enterprise.CoreData.Dto;

namespace Enterprise.CoreData.Converters
{
    public class ConverterFactory
    {
        public static IConverterItem<TRepo, TProxy> GetConverter<TRepo, TProxy>()
        {
            
            Type t = typeof(TRepo);
            if (t == typeof(Author))
                return (IConverterItem<TRepo,TProxy>) new AuthorConverter();
            if (t == typeof(Book))
                return (IConverterItem<TRepo, TProxy>)new BookConverter();
            if (t == typeof(Item))
                return (IConverterItem<TRepo, TProxy>)new ItemConverter();
            if (t == typeof(Order))
                return (IConverterItem<TRepo, TProxy>)new OrderConverter();
            if (t == typeof(Publisher))
                return (IConverterItem<TRepo, TProxy>)new PublisherConverter();
            if (t == typeof(Email))
                return (IConverterItem<TRepo, TProxy>)new EmailConverter();
            if (t == typeof(Reader))
                return (IConverterItem<TRepo, TProxy>)new ReaderConverter();
            if (t == typeof(ReadingCart))
                return (IConverterItem<TRepo, TProxy>)new ReadingCartConverter();
            if (t == typeof(Email))
                return (IConverterItem<TRepo, TProxy>)new EmailConverter();
            if (t == typeof(ReaderCartSelection))
                return (IConverterItem<TRepo, TProxy>)new ReadingCartSelectionConverter();
            if (t == typeof(BookToAuthor))
                return (IConverterItem<TRepo, TProxy>)new BookToAuthorConverter();
            if (t == typeof(ApprovedOrder))
                return (IConverterItem<TRepo, TProxy>)new ApprovedOrderConverter();
           
            throw new ArgumentException("Can't generate converter for type ");

        }
    }

}
