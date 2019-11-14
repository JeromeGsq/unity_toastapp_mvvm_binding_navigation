using System;
using System.ComponentModel;

namespace Toastapp.MVVM
{
    public interface IViewModel
    {
        bool IsInBackground
        {
            get;
        }

        object Parameters
        {
            get; set;
        }

        void SetParameters<T>(T parameters);
        void SetInBackground(bool parameters);
    }
}