﻿using System.ComponentModel;
using UnityEngine;

namespace Toastapp.MVVM
{
    public class UnityView : MonoBehaviour, IView
    {
        private IView viewComponent;
        private INotifyPropertyChanged viewModelComponent;

        protected virtual void Awake()
        {
            this.Init();
        }

        protected virtual void Start()
        {
        }

        protected virtual void OnEnable()
        {
        }

        protected virtual void Update()
        {
        }

        protected virtual void LateUpdate()
        {
        }

        protected virtual void FixedUpdate()
        {
        }

        protected virtual void OnDisable()
        {
        }

        protected virtual void OnDestroy()
        {
            this.UnSubscribe();
        }

        protected virtual void OnGUI()
        {
        }

        protected void Init()
        {
            var components = this.GetComponents(typeof(UnityEngine.Component));

            this.viewComponent = default;
            this.viewModelComponent = default;

            foreach (var component in components)
            {
                var interfaces = component.GetType().GetInterfaces();
                foreach (var inter in interfaces)
                {
                    // If this gameobject contains a component of type IView...
                    if (inter.Equals(typeof(IView)))
                    {
                        this.viewComponent = component as IView;
                    }

                    // And if this gameobject contains a component of type IViewModel...
                    if (inter.Equals(typeof(INotifyPropertyChanged)))
                    {
                        this.viewModelComponent = component as INotifyPropertyChanged;
                    }
                }
            }

            if (this.viewComponent != default(IView) && this.viewModelComponent != default(INotifyPropertyChanged))
            {
                // ... subscribe 
                this.viewModelComponent.PropertyChanged += this.viewComponent.OnPropertyChanged;
            }
        }

        public virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs property)
        {
        }

        private void UnSubscribe()
        {
            if (this.viewModelComponent != null && this.viewComponent != null)
            {
                this.viewModelComponent.PropertyChanged -= this.viewComponent.OnPropertyChanged;
            }
        }
    }
}