using UnityEngine;

[RequireComponent(typeof(SplashscreenViewModel))]
public class SplashscreenView : BaseView<SplashscreenViewModel>
{
    private bool canNext = false;

    public override void Awake()
    {
        base.Awake();

        this.StartCoroutine(CoroutineUtils.DelaySeconds(() => this.ViewModel.Next(), 1f));
    }
}
