using System.ComponentModel;
using UnityEngine;

namespace Toastapp.MVVM
{
    public class UnityView : MonoBehaviour, IView
    {
        public virtual void Awake()
        {
            this.Init();
        }

        public virtual void Start()
        {
        }

        public virtual void OnEnable()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void LateUpdate()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void OnDisable()
        {
        }

        public virtual void OnDestroy()
        {
        }

        public virtual void OnGUI()
        {
        }

        public virtual void OnTriggerEnter()
        {
        }

        public virtual void OnTriggerExit()
        {
        }

        public virtual void OnCollisionEnter()
        {
        }

        public virtual void OnCollisionExit()
        {
        }

        protected void Init()
        {
            var components = this.GetComponents(typeof(UnityEngine.Component));

            IView viewComponent = default(IView);
            INotifyPropertyChanged viewModelComponent = default(INotifyPropertyChanged);

            foreach (var component in components)
            {
                var interfaces = component.GetType().GetInterfaces();
                foreach (var inter in interfaces)
                {
                    // If this gameobject contains a component of type IView...
                    if (inter.Equals(typeof(IView)))
                    {
                        viewComponent = component as IView;
                    }

                    // And if this gameobject contains a component of type IViewModel...
                    if (inter.Equals(typeof(INotifyPropertyChanged)))
                    {
                        viewModelComponent = component as INotifyPropertyChanged;
                    }
                }
            }

            if (viewComponent != default(IView) && viewModelComponent != default(INotifyPropertyChanged))
            {
                // ... subscribe 
                viewModelComponent.PropertyChanged += viewComponent.OnPropertyChanged;
            }
        }

        public virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs property)
        {
        }
    }
}