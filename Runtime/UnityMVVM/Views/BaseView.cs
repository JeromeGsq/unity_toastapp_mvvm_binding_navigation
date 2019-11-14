using System.ComponentModel;
using Toastapp.MVVM;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(GraphicRaycaster))]
public class BaseView<T> : UnityView where T : UnityViewModel
{
    protected Canvas Canvas
    {
        get;
        set;
    }

    protected GraphicRaycaster GraphicRaycaster
    {
        get;
        set;
    }

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

        this.Canvas = this.GetComponent<Canvas>();
        this.GraphicRaycaster = this.GetComponent<GraphicRaycaster>();
        this.RectTransform = this.GetComponent<RectTransform>();
        this.ViewModel = this.GetComponent<T>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnPropertyChanged(object sender, PropertyChangedEventArgs property)
    {
        base.OnPropertyChanged(sender, property);

        if (this.ViewModel != null && property.PropertyName == nameof(this.ViewModel.IsInBackground))
        {
            // Improve perfomance by disabling components and lowering draw calls
            // If ViewModel is null, keep components enable
            this.Canvas.enabled = !this.ViewModel?.IsInBackground ?? true;
            this.GraphicRaycaster.enabled = !this.ViewModel?.IsInBackground ?? true;
        }
    }
}
