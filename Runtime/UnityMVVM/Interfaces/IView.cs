using System.ComponentModel;

namespace Toastapp.MVVM
{
    public interface IView
    {
        void OnPropertyChanged(object sender, PropertyChangedEventArgs property);
    }
}