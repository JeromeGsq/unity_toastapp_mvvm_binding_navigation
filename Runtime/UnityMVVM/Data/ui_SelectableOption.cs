using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toastapp.MVVM
{
    [System.Serializable]
    public abstract class ui_SelectableOption : ScriptableObject
    {

        public bool IsToggled = false;
        public bool DefaultToggleValue = false;

        public abstract string GetDisplayableInfo();
    }
}