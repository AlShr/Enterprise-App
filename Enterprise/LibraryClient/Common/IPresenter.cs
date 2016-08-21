
namespace LibraryClient.Common
{
    public interface IPresenter
    {
        void Run();
    }

    public interface IPresenter<in TArg>
    {
        void Run(TArg argument);
    }

    public interface IPresenter<in TArg1, in TArg2>
    {
        void Run(TArg1 argument1, TArg2 argument2);
    }
}
