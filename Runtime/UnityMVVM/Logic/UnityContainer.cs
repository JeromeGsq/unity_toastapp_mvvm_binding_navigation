using System;

namespace Toastapp.MVVM
{
    public class UnityContainer : IUnityContainerIoC
    {
        public T Resolve<T>() where T : class, IUnityIoC
        {
            return Activator.CreateInstance<T>();
        }

        public T Resolve<T>(Type type) where T : class, IUnityIoC
        {
            return (T)Activator.CreateInstance(type);
        }
    }
}