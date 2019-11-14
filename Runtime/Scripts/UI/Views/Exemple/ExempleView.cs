using UnityEngine;

[RequireComponent(typeof(ExempleViewModel))]
public class ExempleView : BaseView<ExempleViewModel>
{
    private bool canNext = false;

    public override void ShowView()
    {
        base.Awake();
        this.ViewModel.Next();
    }
}
