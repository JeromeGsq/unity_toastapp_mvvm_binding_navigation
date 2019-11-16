using UnityWeld.Binding;

namespace Toastapp.MVVM.Exemple
{
    [Binding]
    public class ExempleViewModel : BaseViewModel
    {
        private string title;

        [Binding]
        public string Title
        {
            get => this.title;
            set => this.Set(ref this.title, value, nameof(this.Title));
        }

        public void OnEnable()
        {
            this.Title = "ExempleViewModel from viewmodel";
        }

        [Binding]
        public void Next()
        {
            NavigationService.Get.ShowViewModel(typeof(ExempleViewModel));
        }
    }
}
