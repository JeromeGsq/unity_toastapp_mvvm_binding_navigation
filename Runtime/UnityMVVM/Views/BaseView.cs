using Toastapp.MVVM;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(RectTransform))]
public class BaseView<T> : UnityView
{
    protected RectTransform RectTransform
    {
        get;
        set;
    }

    protected T ViewModel
    {
        get;
        set;
    }

    protected override void Awake()
    {
        base.Awake();

        this.RectTransform = this.GetComponent<RectTransform>();
        this.ViewModel = this.GetComponent<T>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.ShowView();
    }

    public virtual void ShowView()
    {
    }
}
