using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Toastapp.MVVM;
using UnityEngine;

public class App : MonoBehaviour
{
    protected void Start()
    {
        CultureInfo ci = new CultureInfo("fr-FR");
        Thread.CurrentThread.CurrentCulture = ci;
        Thread.CurrentThread.CurrentUICulture = ci;

        NavigationService.Get.ShowViewModel(typeof(SplashscreenViewModel));
    }
}
