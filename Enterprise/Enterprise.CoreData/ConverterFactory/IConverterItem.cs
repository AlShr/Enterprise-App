

namespace Enterprise.CoreData.ConverterFactory
{
    public interface IConverterItem<TRepo,TProxy>
    {
         TRepo ToRepoItem(TProxy param, bool isBase = false);
         TProxy ToProxyItem(TRepo param, bool isBase = false);
    }
}
