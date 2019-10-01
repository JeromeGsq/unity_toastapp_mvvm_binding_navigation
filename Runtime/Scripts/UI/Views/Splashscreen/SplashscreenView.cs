using UnityEngine;

[RequireComponent(typeof(SplashscreenViewModel))]
public class SplashscreenView : BaseView<SplashscreenViewModel>
{
    private bool canNext = false;

    public override void Awake()
    {
        base.Awake();

        this.ViewModel.Next();
    }
}
