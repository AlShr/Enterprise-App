using Enterprise.CoreData.ConverterFactory;
using System;
using Enterprise.Model;

namespace Enterprise.Services.Converters
{
    public class ConverterFactory
    {
        public static IConverterItem<TRepo, TProxy> GetConverter<TRepo, TProxy>()
        {

            Type t = typeof(TRepo);
            if (t == typeof(AuthorModel))
                return (IConverterItem<TRepo, TProxy>)new AuthorModelConverter();
            if (t == typeof(BookModel))
                return (IConverterItem<TRepo, TProxy>)new BookModelConverter();
            if (t == typeof(BookToAuthorModel))
                return (IConverterItem<TRepo, TProxy>)new BookToAuthorModelConverter();
            if (t == typeof(PublisherModel))
                return (IConverterItem<TRepo, TProxy>)new PublisherModelConverter();
            if (t == typeof(ItemModel))
                return (IConverterItem<TRepo, TProxy>)new ItemModelConverter();
            if (t == typeof(EmailModel))
                return (IConverterItem<TRepo, TProxy>)new EmailModelConverter();
            if (t == typeof(ReaderModel))
                return (IConverterItem<TRepo, TProxy>)new ReaderModelConverter();
            if (t == typeof(ReaderCartSelectionModel))
                return (IConverterItem<TRepo, TProxy>)new ReadingCartSelectionModelConverter();
            if (t == typeof(ApprovedOrderModel))
                return (IConverterItem<TRepo, TProxy>)new ApprovedOrderConverter();
            

            throw new ArgumentException("Can't generate converter for type ");

        }
    }
}
