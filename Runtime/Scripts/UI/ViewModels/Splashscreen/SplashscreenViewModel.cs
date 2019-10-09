using UnityWeld.Binding;

[Binding]
public class SplashscreenViewModel : BaseViewModel
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
        this.Title = "Splashscreen from viewmodel";
    }

    public void Next()
    {
        // Navigate to next VM
        // NavigationService.Get.ShowViewModel(typeof(MenuViewModel));
    }
}
