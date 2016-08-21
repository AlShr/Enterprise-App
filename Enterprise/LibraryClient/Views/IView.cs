using System;

namespace LibraryClient.Views
{
    public interface IView
    {
        void Show();
        void Close();
        event EventHandler Load;
    }
}
