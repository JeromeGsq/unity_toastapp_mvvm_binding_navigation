using UnityEngine;
using System.Collections;

namespace Toastapp.MVVM
{
    [System.Serializable]
    public abstract class ui_Coroutine : MonoBehaviour
    {

        public abstract IEnumerator Run();
        public abstract IEnumerator Stop();
    }
}