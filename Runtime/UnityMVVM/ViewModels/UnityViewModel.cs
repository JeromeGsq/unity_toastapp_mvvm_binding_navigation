using System;
using System.ComponentModel;
using UnityEngine;

namespace Toastapp.MVVM
{
    public abstract class UnityViewModel : MonoBehaviour, IViewModel, INotifyPropertyChanged
    {
        public object Parameters
        {
            get;
            set;
        }

        public bool IsInBackground
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetParameters<T>(T parameters)
        {
            this.Parameters = parameters;
            this.OnParametersChanged();
        }

        protected virtual void OnParametersChanged()
        {
        }

        protected void Set<T>(ref T property, object value, string propertyName)
        {
            property = (T)value;
            this.RaisePropertyChanged(propertyName);
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RaiseAllPropertyChanged(Type viewModelType)
        {
            foreach (var property in viewModelType.GetProperties())
            {
                this.RaisePropertyChanged(property.Name);
            }
        }
    }
}