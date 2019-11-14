using System.Globalization;
using System.Threading;
using UnityEngine;

namespace Toastapp.MVVM.Exemple
{
    public class AppExemple : MonoBehaviour
    {
        protected void Start()
        {
            CultureInfo ci = new CultureInfo("fr-FR");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            NavigationService.Get.ShowViewModel(typeof(ExempleViewModel));
        }
    }
}

