using UnityEngine;

namespace Toastapp.MVVM.Exemple
{
    [RequireComponent(typeof(ExempleViewModel))]
    public class ExempleView : BaseView<ExempleViewModel>
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            this.ViewModel.Next();
        }
    }
}